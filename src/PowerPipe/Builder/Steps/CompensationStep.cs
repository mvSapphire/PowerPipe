using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

/// <summary>
/// Represents a compensation step used in pipeline processing to perform compensation actions.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
internal class CompensationStep<TContext> : IPipelineCompensationStep<TContext>
{
    private readonly Lazy<IPipelineCompensationStep<TContext>> _step;

    /// <summary>
    /// Gets a value indicating whether the compensation step has been executed.
    /// </summary>
    public bool IsCompensated { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompensationStep{TContext}"/> class.
    /// </summary>
    /// <param name="factory">A factory function to create the compensation step.</param>
    internal CompensationStep(Func<IPipelineCompensationStep<TContext>> factory)
    {
        _step = new Lazy<IPipelineCompensationStep<TContext>>(() =>
        {
            var instance = factory();
            return instance;
        });
    }

    /// <summary>
    /// Performs compensation actions asynchronously.
    /// </summary>
    /// <param name="context">The context on which the compensation action is performed.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous compensation operation.</returns>
    public async Task CompensateAsync(TContext context, CancellationToken cancellationToken)
    {
        IsCompensated = true;

        await _step.Value.CompensateAsync(context, cancellationToken);
    }
}