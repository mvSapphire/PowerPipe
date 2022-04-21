using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class WhenPipelineStep<TContext, TResult> : IPipelineStep<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly Func<bool> _predicate;
    private readonly PipelineBuilder<TContext, TResult> _pipelineBuilder;

    public WhenPipelineStep(Func<bool> predicate, PipelineBuilder<TContext, TResult> pipelineBuilder)
    {
        _predicate = predicate;
        _pipelineBuilder = pipelineBuilder;
    }

    public IPipelineStep<TContext, TResult> NextStep { get; set; }

    public async Task ExecuteAsync(TContext context)
    {
        if (_predicate())
        {
            await _pipelineBuilder.Build().RunAsync(returnResult: false);
        }

        await NextStep.ExecuteAsync(context);
    }
}