using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe;

/// <summary>
/// Represents a pipeline for executing a series of steps.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
/// <typeparam name="TResult">The type of result returned by the pipeline.</typeparam>
public class Pipeline<TContext, TResult> : IPipeline<TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly TContext _context;
    private readonly IPipelineStep<TContext> _initStep;

    /// <summary>
    /// Initializes a new instance of the <see cref="Pipeline{TContext, TResult}"/> class.
    /// </summary>
    /// <param name="context">The context for the pipeline.</param>
    /// <param name="steps">The list of pipeline steps to execute.</param>
    public Pipeline(TContext context, IReadOnlyList<IPipelineStep<TContext>> steps)
    {
        _context = context;

        _initStep = steps[0];

        SetupSteps(steps);
    }

    /// <summary>
    /// Runs the pipeline asynchronously with the option to return a result.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <param name="returnResult">A flag indicating whether to return a result.</param>
    /// <returns>A task representing the asynchronous operation and optionally the result.</returns>
    public async Task<TResult> RunAsync(CancellationToken cancellationToken, bool returnResult = true)
    {
        await _initStep.ExecuteAsync(_context, cancellationToken);

        // to avoid multiple result calls in nested pipelines
        return returnResult ? _context.GetPipelineResult() : null;
    }

    private static void SetupSteps(IReadOnlyList<IPipelineStep<TContext>> steps)
    {
        for (var i = 0; i < steps.Count - 1; i++)
        {
            steps[i].NextStep = steps[i + 1];
        }
    }
}