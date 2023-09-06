using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

internal class CompensationStep<TContext> : IPipelineCompensationStep<TContext>
{
    private readonly Lazy<IPipelineCompensationStep<TContext>> _step;

    public bool IsCompensated { get; private set; }

    internal CompensationStep(Func<IPipelineCompensationStep<TContext>> factory)
    {
        _step = new Lazy<IPipelineCompensationStep<TContext>>(() =>
        {
            var instance = factory();
            return instance;
        });
    }

    public async Task CompensateAsync(TContext context, CancellationToken cancellationToken)
    {
        IsCompensated = true;

        await _step.Value.CompensateAsync(context, cancellationToken);
    }
}