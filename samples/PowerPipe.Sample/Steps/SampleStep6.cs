using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleStep6 : IPipelineStep<SamplePipelineContext>
{
    public IPipelineStep<SamplePipelineContext> NextStep { get; set; }
    public async ValueTask ExecuteAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        await NextStep.ExecuteAsync(context, cancellationToken);
    }
}
