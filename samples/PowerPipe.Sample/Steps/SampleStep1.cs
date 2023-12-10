using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleStep1 : IPipelineStep<SamplePipelineContext>
{
    public IPipelineStep<SamplePipelineContext> NextStep { get; set; }
    public async ValueTask ExecuteAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleStep1)} Executed");
        await NextStep.ExecuteAsync(context, cancellationToken);
    }
}
