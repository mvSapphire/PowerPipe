using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPipe.Interfaces;

namespace EasyPipe;

public class Pipeline : IPipeline
{
    private readonly PipelineContext _context;
    private readonly IPipelineStep _initStep;

    public Pipeline(PipelineContext context, IReadOnlyCollection<IPipelineStep> steps)
    {
        _context = context;

        _initStep = steps.First();

        SetNextSteps(steps);
    }

    public async Task<PipelineResult> RunAsync(bool returnResult = true)
    {
        await _initStep.ExecuteAsync(_context);

        // to avoid multiple result calls in nested pipelines
        return returnResult ? _context.GetPipelineResult() : null;
    }

    private static void SetNextSteps(IReadOnlyCollection<IPipelineStep> steps)
    {
        for (var i = 0; i < steps.Count - 1; i++)
        {
            steps.ElementAt(i).NextStep = steps.ElementAt(i + 1);
        }
    }
}