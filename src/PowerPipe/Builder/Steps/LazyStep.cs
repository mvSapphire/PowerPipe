using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

internal class LazyStep<TContext> : InternalStep<TContext>
{
    private readonly Lazy<IStepBase<TContext>> _step;

    internal LazyStep(Func<IStepBase<TContext>> factory)
    {
        _step = new Lazy<IStepBase<TContext>>(() =>
        {
            var instance = factory();

            if (instance is IPipelineStep<TContext> step)
                step.NextStep = NextStep;

            return instance;
        });
    }

    protected override async Task ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        StepExecuted = true;

        await _step.Value.ExecuteAsync(context, cancellationToken);
    }
}