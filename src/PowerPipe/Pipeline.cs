using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe;

public class Pipeline<TContext, TResult> : IPipeline<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly TContext _context;
    private readonly IPipelineStep<TContext, TResult> _initStep;

    public Pipeline(TContext context, IReadOnlyCollection<IPipelineStep<TContext, TResult>> steps)
    {
        _context = context;

        _initStep = steps.First();

        SetNextSteps(steps);
    }

    public async Task<TResult> RunAsync(bool returnResult = true)
    {
        await _initStep.ExecuteAsync(_context);

        // to avoid multiple result calls in nested pipelines
        return returnResult ? _context.GetPipelineResult() : null;
    }

    private static void SetNextSteps(IReadOnlyCollection<IPipelineStep<TContext, TResult>> steps)
    {
        for (var i = 0; i < steps.Count - 1; i++)
        {
            steps.ElementAt(i).NextStep = steps.ElementAt(i + 1);
        }
    }
}