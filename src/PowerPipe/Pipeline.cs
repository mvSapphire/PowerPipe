using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Builder.Steps;
using PowerPipe.Interfaces;

namespace PowerPipe;

public class Pipeline<TContext, TResult> : IPipeline<TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly TContext _context;
    private readonly IPipelineStep<TContext> _initStep;

    public Pipeline(TContext context, IReadOnlyList<IPipelineStep<TContext>> steps)
    {
        _context = context;

        _initStep = steps[0];

        SetupSteps(steps);
    }

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
            var currentStep = steps[i] as InternalStep<TContext>;
            var nextStep = steps[i + 1] as InternalStep<TContext>;

            currentStep!.NextStep = nextStep;

            if (nextStep!.CompensationStep is CompensationStep<TContext> nextStepCompensation)
            {
                nextStepCompensation.NextCompensationStep = currentStep.CompensationStep;
            }
        }
    }
}