using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class IfPipelineStep<TContext, TResult> : IPipelineStep<TContext>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly Func<bool> _predicate;
    private readonly PipelineBuilder<TContext, TResult> _pipelineBuilder;

    public IfPipelineStep(Func<bool> predicate, PipelineBuilder<TContext, TResult> pipelineBuilder)
    {
        _predicate = predicate;
        _pipelineBuilder = pipelineBuilder;
    }

    public IPipelineStep<TContext> NextStep { get; set; }

    public async Task ExecuteAsync(TContext context, CancellationToken cancellationToken)
    {
        if (_predicate())
        {
            await _pipelineBuilder.Build().RunAsync(cancellationToken, returnResult: false);
        }

        await NextStep.ExecuteAsync(context, cancellationToken);
    }
}