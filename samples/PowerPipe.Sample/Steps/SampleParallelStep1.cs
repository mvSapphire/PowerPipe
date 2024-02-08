using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleParallelStep1 : IPipelineParallelStep<SamplePipelineContext>
{
    public ValueTask ExecuteAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }
}
