using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestStep3 : IPipelineStep<TestPipelineContext>
{
    public static int CreationCount { get; private set; }

    public IPipelineStep<TestPipelineContext> NextStep { get; set; }
    
    public TestStep3()
    {
        CreationCount++;
    }

    public virtual Task ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken) =>
        throw new InvalidOperationException();
}