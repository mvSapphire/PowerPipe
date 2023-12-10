using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestStep1 : IPipelineStep<TestPipelineContext>
{
    public static int CreationCount { get; private set; }

    public IPipelineStep<TestPipelineContext> NextStep { get; set; }

    public TestStep1()
    {
        CreationCount++;
    }

    public virtual async ValueTask ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken)
    {
        context.Step1RunCount++;
        await NextStep.ExecuteAsync(context, cancellationToken);
    }
}
