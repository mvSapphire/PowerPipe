using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

internal class AddWhenStep<TContext> : IPipelineStep<TContext>
{
    private readonly IPipelineStep<TContext> _step;
    private readonly Predicate<TContext> _predicate;

    public IPipelineStep<TContext> NextStep { get; set; }

    internal AddWhenStep(Predicate<TContext> predicate, IPipelineStep<TContext> step)
    {
        _predicate = predicate;
        _step = step;
    }

    public async Task ExecuteAsync(TContext context)
    {
        if (_predicate(context))
        {
            _step.NextStep = NextStep;

            await _step.ExecuteAsync(context);
        }
        else
        {
            await NextStep.ExecuteAsync(context);
        }
    }
}