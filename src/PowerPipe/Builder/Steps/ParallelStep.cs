using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PowerPipe.Builder.Steps;

/// <summary>
/// Represents a parallel execution pipeline step.
/// <para>
/// Thread safety: all steps within a parallel branch share the same <typeparamref name="TContext"/> instance.
/// Step implementations added via <c>Parallel()</c> must not mutate shared context state without
/// proper synchronization (e.g., <c>Interlocked</c>, <c>lock</c>, or concurrent collections).
/// Read-only access to context is safe. If steps need to write results, use thread-safe data structures
/// or collect results independently and merge after parallel execution completes.
/// </para>
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
/// <typeparam name="TResult">The type of result returned by the pipeline.</typeparam>
internal class ParallelStep<TContext, TResult> : InternalStep<TContext>
    where TContext : PipelineContext<TResult>
{
    private readonly int _maxDegreeOfParallelism;
    private readonly PipelineBuilder<TContext, TResult> _pipelineBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParallelStep{TContext, TResult}"/> class.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism for the parallel execution.</param>
    /// <param name="pipelineBuilder">The builder for the sub-pipeline to execute in parallel.</param>
    /// <param name="loggerFactory">A logger factory</param>
    public ParallelStep(
        int maxDegreeOfParallelism, PipelineBuilder<TContext, TResult> pipelineBuilder, ILoggerFactory loggerFactory)
    {
        _maxDegreeOfParallelism = maxDegreeOfParallelism;
        _pipelineBuilder = pipelineBuilder;

        Logger = loggerFactory?.CreateLogger<ParallelStep<TContext, TResult>>();
    }

    /// <summary>
    /// Executes the parallel execution pipeline step asynchronously.
    /// </summary>
    /// <param name="context">The context on which the step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async ValueTask ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        Logger?.LogDebug("Executing parallel steps.");

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
