using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests.Steps;

public class TestStep1 : IPipelineStep<TestPipelineContext>
{
    public IPipelineStep<TestPipelineContext> NextStep { get; set; }

    public Task ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken)
    {
        if (context != null)
        {
            context.Step1RunCount++;
        }
        
        return Task.CompletedTask;
    }
}