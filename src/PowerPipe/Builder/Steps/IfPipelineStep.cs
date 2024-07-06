using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PowerPipe.Builder.Steps;

/// <summary>
/// Represents a conditional pipeline step that executes a sub-pipeline based on a provided predicate.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
/// <typeparam name="TResult">The type of result returned by the sub-pipeline.</typeparam>
internal class IfPipelineStep<TContext, TResult> : InternalStep<TContext>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly Predicate<TContext> _predicate;
    private readonly PipelineBuilder<TContext, TResult> _pipelineBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="IfPipelineStep{TContext, TResult}"/> class.
    /// </summary>
    /// <param name="predicate">The predicate to determine whether to execute the sub-pipeline.</param>
    /// <param name="pipelineBuilder">The builder for the sub-pipeline to execute.</param>
    /// <param name="loggerFactory">A logger factory</param>
    public IfPipelineStep(
        Predicate<TContext> predicate, PipelineBuilder<TContext, TResult> pipelineBuilder, ILoggerFactory loggerFactory)
    {
        _predicate = predicate;
        _pipelineBuilder = pipelineBuilder;

        Logger = loggerFactory?.CreateLogger<IfPipelineStep<TContext, TResult>>();
    }

    /// <summary>
    /// Executes the conditional pipeline step asynchronously based on the provided context and predicate.
    /// </summary>
    /// <param name="context">The context on which the conditional pipeline step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async ValueTask ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        if (_predicate(context))
        {
            Logger?.LogDebug("Executing internal pipeline.");

            StepExecuted = true;

            await _pipelineBuilder.Build().RunAsync(cancellationToken, returnResult: false);
        }

        if (NextStep is not null)
            await NextStep.ExecuteAsync(context, cancellationToken);
    }
}
