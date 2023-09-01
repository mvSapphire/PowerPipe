using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

internal class LazyStep<TContext> : InternalStep<TContext>
{
    private readonly Lazy<IPipelineStep<TContext>> _step;

    internal LazyStep(Func<IPipelineStep<TContext>> factory)
    {
        _step = new Lazy<IPipelineStep<TContext>>(() =>
        {
            var instance = factory();
            instance.NextStep = NextStep;
            return instance;
        });
    }

    protected override async Task ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        IsExecuted = true;

        await _step.Value.ExecuteAsync(context, cancellationToken);
    }
}