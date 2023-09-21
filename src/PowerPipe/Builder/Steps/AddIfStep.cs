using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

internal class AddIfStep<TContext> : InternalStep<TContext>
{
    private readonly IPipelineStep<TContext> _step;
    private readonly Predicate<TContext> _predicate;

    internal AddIfStep(Predicate<TContext> predicate, IPipelineStep<TContext> step)
    {
        _predicate = predicate;
        _step = step;
    }

    protected override async Task ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        if (_predicate(context))
        {
            StepExecuted = true;

            _step.NextStep = NextStep;

            await _step.ExecuteAsync(context, cancellationToken);
        }
        else
        {
            if (NextStep is not null)
                await NextStep.ExecuteAsync(context, cancellationToken);
        }
    }
}