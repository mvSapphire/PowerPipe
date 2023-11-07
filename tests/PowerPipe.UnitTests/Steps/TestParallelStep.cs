using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestParallelStep : IPipelineParallelStep<TestPipelineContext>
{
    public Task ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken)
    {
        context.ParallelStepRunCount++;

        return Task.CompletedTask;
    }

    public IPipelineStep<TestPipelineContext> NextStep { get; set; }
}
