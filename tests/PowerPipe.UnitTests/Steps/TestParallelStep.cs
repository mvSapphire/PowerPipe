using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestParallelStep : IPipelineParallelStep<TestPipelineContext>
{
    public ValueTask ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken)
    {
        context.ParallelStepRunCount++;

        return ValueTask.CompletedTask;
    }

    public IPipelineStep<TestPipelineContext> NextStep { get; set; }
}
