using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PowerPipe.Exceptions;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

/// <summary>
/// Represents an abstract base class for pipeline steps.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
internal abstract class InternalStep<TContext> : IPipelineStep<TContext>, IPipelineParallelStep<TContext>
{
    /// <inheritdoc/>
    public IPipelineStep<TContext> NextStep { get; set; }

    /// <summary>
    /// Gets or sets the compensation step associated with this step.
    /// </summary>
    public CompensationStep<TContext> CompensationStep { get; set; }

    /// <summary>
    /// Gets or init a logger.
    /// </summary>
    protected ILogger Logger { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this step has been executed.
    /// </summary>
    protected virtual bool StepExecuted { get; set; }

    /// <summary>
    /// Gets or sets the error handling behavior for this step.
    /// </summary>
    protected virtual PipelineStepErrorHandling? ErrorHandlingBehaviour { get; private set; }

    /// <summary>
    /// Gets or sets the retry interval for error handling.
    /// </summary>
    protected virtual TimeSpan? RetryInterval { get; private set; }

    /// <summary>
    /// Gets or sets the maximum number of retries for error handling.
    /// </summary>
    protected virtual int? MaxRetryCount { get; private set; }

    /// <summary>
    /// Gets or sets a predicate to determine whether error handling should be applied.
    /// </summary>
    protected virtual Predicate<TContext> ErrorHandlingPredicate { get; private set; }

    private int RetryCount { get; set; }

    private bool? ErrorHandledSucceed { get; set; }

    private bool AllowedToCompensate => StepExecuted && ErrorHandledSucceed == false && CompensationStep?.IsCompensated == false;

    /// <summary>
    /// Configures error handling behavior for this step.
    /// </summary>
    /// <param name="errorHandling">The error handling behavior to configure.</param>
    /// <param name="retryInterval">The retry interval for retrying the step in case of an error.</param>
    /// <param name="maxRetryCount">The maximum number of times to retry the step.</param>
    /// <param name="predicate">A predicate to determine whether error handling should be applied.</param>
    public void ConfigureErrorHandling(PipelineStepErrorHandling errorHandling, TimeSpan? retryInterval, int? maxRetryCount, Predicate<TContext> predicate)
    {
        if (errorHandling is PipelineStepErrorHandling.Retry)
        {
            retryInterval ??= TimeSpan.FromSeconds(1);
            maxRetryCount ??= 1;
        }

        ErrorHandlingBehaviour = errorHandling;
        RetryInterval = retryInterval;
        MaxRetryCount = maxRetryCount;
        ErrorHandlingPredicate = predicate;
    }

    /// <inheritdoc/>
    public async ValueTask ExecuteAsync(TContext context, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            await ExecuteInternalAsync(context, cancellationToken);
        }
        catch (Exception e)
        {
            if (e is PipelineExecutionException or OperationCanceledException)
            {
                ErrorHandledSucceed = false;
                throw;
            }

            ErrorHandledSucceed = await HandleExceptionAsync(context, cancellationToken);

            if (!ErrorHandledSucceed.Value)
                throw new PipelineExecutionException(e);
        }
        finally
        {
            if (AllowedToCompensate)
                await CompensationStep.CompensateAsync(context, cancellationToken);
        }
    }

    /// <summary>
    /// Executes the internal logic of the pipeline step asynchronously.
    /// </summary>
    /// <param name="context">The context on which the step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual ValueTask ExecuteInternalAsync(TContext context, CancellationToken cancellationToken) =>
        ValueTask.CompletedTask;

    /// <summary>
    /// Handles an exception that occurred during the step's execution.
    /// </summary>
    /// <param name="context">The context on which the step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. Returns true if the exception is handled; otherwise, false.</returns>
    protected virtual async ValueTask<bool> HandleExceptionAsync(TContext context, CancellationToken cancellationToken)
    {
        if (ErrorHandlingPredicate is not null && !ErrorHandlingPredicate(context))
            return false;

        switch (ErrorHandlingBehaviour)
        {
            case PipelineStepErrorHandling.Suppress:
                return true;

            case PipelineStepErrorHandling.Retry:
                break;

            case null:
                return false;
        }

        if (RetryCount >= (MaxRetryCount ?? 1))
            return false;

        RetryCount++;
        await Task.Delay(RetryInterval ?? TimeSpan.FromSeconds(1), cancellationToken);

        await ExecuteAsync(context, cancellationToken);

        return true;
    }
}

/// <summary>
/// Represents error handling behavior for a pipeline step.
/// </summary>
public enum PipelineStepErrorHandling
{
    /// <summary>
    /// Suppresses errors and continues pipeline execution.
    /// </summary>
    Suppress = 0,

    /// <summary>
    /// Retries the step execution in case of an error.
    /// </summary>
    Retry = 1,
}
