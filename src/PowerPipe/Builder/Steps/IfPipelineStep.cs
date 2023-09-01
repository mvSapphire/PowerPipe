using System;
using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Builder.Steps;

internal class IfPipelineStep<TContext, TResult> : InternalStep<TContext>
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

    protected override async Task ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        if (_predicate())
        {
            IsExecuted = true;

            await _pipelineBuilder.Build().RunAsync(cancellationToken, returnResult: false);
        }

        await NextStep.ExecuteAsync(context, cancellationToken);
    }
}