using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class WhenActionStep : IPipelineStep
{
    private readonly Predicate<PipelineContext> _predicate;
    private readonly Action<PipelineContext> _action;

    public WhenActionStep(Predicate<PipelineContext> predicate, Action<PipelineContext> action)
    {
        _predicate = predicate;
        _action = action;
    }

    public IPipelineStep NextStep { get; set; }

    public async Task ExecuteAsync(PipelineContext context)
    {
        if (_predicate(context))
        {
            _action(context);
        }

        await NextStep.ExecuteAsync(context);
    }
}