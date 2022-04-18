using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class WhenPipelineStep<TContext> : IPipelineStep<TContext>
    where TContext : PipelineContext
{
    private readonly Func<bool> _predicate;
    private readonly PipelineBuilder<TContext> _pipelineBuilder;

    public WhenPipelineStep(Func<bool> predicate, PipelineBuilder<TContext> pipelineBuilder)
    {
        _predicate = predicate;
        _pipelineBuilder = pipelineBuilder;
    }

    public IPipelineStep<TContext> NextStep { get; set; }

    public async Task ExecuteAsync(TContext context)
    {
        if (_predicate())
        {
            await _pipelineBuilder.Build().RunAsync(returnResult: false);
        }

        await NextStep.ExecuteAsync(context);
    }
}