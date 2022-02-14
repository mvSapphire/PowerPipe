using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class WhenPipelineStep : IPipelineStep
{
    private readonly Func<bool> _predicate;
    private readonly PipelineBuilder _pipelineBuilder;

    public WhenPipelineStep(Func<bool> predicate, PipelineBuilder pipelineBuilder)
    {
        _predicate = predicate;
        _pipelineBuilder = pipelineBuilder;
    }

    public IPipelineStep NextStep { get; set; }

    public async Task ExecuteAsync(PipelineContext context)
    {
        if (_predicate())
        {
            await _pipelineBuilder.Build().RunAsync(returnResult: false);
        }

        await NextStep.ExecuteAsync(context);
    }
}