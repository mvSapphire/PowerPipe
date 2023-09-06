using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleStep5 : IPipelineStep<SamplePipelineContext>
{
    public IPipelineStep<SamplePipelineContext> NextStep { get; set; }
    public async Task ExecuteAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleStep5)} Executed");
        await NextStep.ExecuteAsync(context, cancellationToken);
    }
}