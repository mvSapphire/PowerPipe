using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestStep2 : IPipelineStep<TestPipelineContext>
{
    public IPipelineStep<TestPipelineContext> NextStep { get; set; }

    public virtual async Task ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken) =>
        throw new InvalidOperationException();
}