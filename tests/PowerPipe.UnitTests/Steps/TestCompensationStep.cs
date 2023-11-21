using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestCompensationStep : IPipelineCompensationStep<TestPipelineContext>
{
    public virtual ValueTask CompensateAsync(TestPipelineContext context, CancellationToken cancellationToken)
    {
        context.CompensationStepRunCount++;

        return ValueTask.CompletedTask;
    }
}
