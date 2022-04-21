using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe;

public class Pipeline<TContext> : IPipeline<TContext>
    where TContext : PipelineContext<Type>
{
    private readonly TContext _context;
    private readonly IPipelineStep<TContext> _initStep;

    public Pipeline(TContext context, IReadOnlyCollection<IPipelineStep<TContext>> steps)
    {
        _context = context;

        _initStep = steps.First();

        SetNextSteps(steps);
    }

    public async Task<Type> RunAsync(bool returnResult = true)
    {
        await _initStep.ExecuteAsync(_context);

        // to avoid multiple result calls in nested pipelines
        return returnResult ? _context.GetPipelineResult() : null;
    }

    private static void SetNextSteps(IReadOnlyCollection<IPipelineStep<TContext>> steps)
    {
        for (var i = 0; i < steps.Count - 1; i++)
        {
            steps.ElementAt(i).NextStep = steps.ElementAt(i + 1);
        }
    }
}