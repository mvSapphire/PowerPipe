using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

/// <inheritdoc/>
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

    /// <inheritdoc/>
    public async ValueTask CompensateAsync(TContext context, CancellationToken cancellationToken)
    {
        IsCompensated = true;

        await _step.Value.CompensateAsync(context, cancellationToken);
    }
}
