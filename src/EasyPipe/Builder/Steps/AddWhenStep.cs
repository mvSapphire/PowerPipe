using System;
using System.Threading.Tasks;
using EasyPipe.Interfaces;

namespace EasyPipe.Builder.Steps;

internal class AddWhenStep : IPipelineStep
{
    private readonly IPipelineStep _step;
    private readonly Predicate<PipelineContext> _predicate;

    public IPipelineStep NextStep { get; set; }

    internal AddWhenStep(Predicate<PipelineContext> predicate, IPipelineStep step)
    {
        _predicate = predicate;
        _step = step;
    }

    public async Task ExecuteAsync(PipelineContext context)
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