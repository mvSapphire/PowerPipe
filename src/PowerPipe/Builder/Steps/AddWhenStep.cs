using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

internal class AddWhenStep<TContext, TResult> : IPipelineStep<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly IPipelineStep<TContext, TResult> _step;
    private readonly Predicate<TContext> _predicate;

    public IPipelineStep<TContext, TResult> NextStep { get; set; }

    internal AddWhenStep(Predicate<PipelineContext<TResult>> predicate, IPipelineStep<TContext, TResult> step)
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