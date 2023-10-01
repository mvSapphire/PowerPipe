using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Builder.Steps;

internal class ParallelStep<TContext, TResult> : InternalStep<TContext>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly int _maxDegreeOfParallelism;
    private readonly PipelineBuilder<TContext, TResult> _pipelineBuilder;

    public ParallelStep(int maxDegreeOfParallelism, PipelineBuilder<TContext, TResult> pipelineBuilder)
    {
        _maxDegreeOfParallelism = maxDegreeOfParallelism;
        _pipelineBuilder = pipelineBuilder;
    }

    protected override async Task ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        StepExecuted = true;

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = _maxDegreeOfParallelism,
            CancellationToken = cancellationToken,
        };

        await Parallel.ForEachAsync(_pipelineBuilder.Steps, parallelOptions, async (step, token) => await step.ExecuteAsync(context, token));

        if (NextStep is not null)
            await NextStep.ExecuteAsync(context, cancellationToken);
    }
}