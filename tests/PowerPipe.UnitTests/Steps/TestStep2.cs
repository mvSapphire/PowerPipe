using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestStep2 : IPipelineStep<TestPipelineContext>
{
    public static int CreationCount { get; private set; }

    public IPipelineStep<TestPipelineContext> NextStep { get; set; }
    
    public TestStep2()
    {
        CreationCount++;
    }

    public virtual ValueTask ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken) =>
        throw new InvalidOperationException();
}
