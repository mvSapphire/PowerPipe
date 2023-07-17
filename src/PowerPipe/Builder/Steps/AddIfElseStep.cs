using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

internal class AddIfElseStep<TContext> : IPipelineStep<TContext>
{
    private readonly IPipelineStep<TContext> _ifStep;
    private readonly IPipelineStep<TContext> _elseStep;
    private readonly Predicate<TContext> _predicate;

    public IPipelineStep<TContext> NextStep { get; set; }

    internal AddIfElseStep(Predicate<TContext> predicate, IPipelineStep<TContext> ifStep, IPipelineStep<TContext> elseStep)
    {
        _predicate = predicate;
        _ifStep = ifStep;
        _elseStep = elseStep;
    }

    public async Task ExecuteAsync(TContext context, CancellationToken cancellationToken)
    {
        if (_predicate(context))
        {
            _ifStep.NextStep = NextStep;

            await _ifStep.ExecuteAsync(context, cancellationToken);
        }
        else
        {
            _elseStep.NextStep = NextStep;

            await _elseStep.ExecuteAsync(context, cancellationToken);
        }
    }
}