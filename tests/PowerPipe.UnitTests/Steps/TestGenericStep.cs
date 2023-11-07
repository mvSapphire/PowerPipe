using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestGenericStep<T>: IPipelineStep<TestPipelineContext> where T : class
{
    public static int CreationCount { get; private set; }

    public IPipelineStep<TestPipelineContext> NextStep { get; set; }

    public TestGenericStep()
    {
        CreationCount++;
    }

    public virtual async Task ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken)
    {
        context.GenericStepRunCount++;
        await NextStep.ExecuteAsync(context, cancellationToken);
    }
}
