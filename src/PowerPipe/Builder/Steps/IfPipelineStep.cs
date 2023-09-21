using System;
using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Builder.Steps;

internal class IfPipelineStep<TContext, TResult> : InternalStep<TContext>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly Predicate<TContext> _predicate;
    private readonly PipelineBuilder<TContext, TResult> _pipelineBuilder;

    public IfPipelineStep(Predicate<TContext> predicate, PipelineBuilder<TContext, TResult> pipelineBuilder)
    {
        _predicate = predicate;
        _pipelineBuilder = pipelineBuilder;
    }

    protected override async Task ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        if (_predicate(context))
        {
            StepExecuted = true;

            await _pipelineBuilder.Build().RunAsync(cancellationToken, returnResult: false);
        }

        if (NextStep is not null)
            await NextStep.ExecuteAsync(context, cancellationToken);
    }
}