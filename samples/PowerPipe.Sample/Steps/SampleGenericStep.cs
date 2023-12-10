using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleGenericStep<T>: IPipelineStep<SamplePipelineContext>
{
    public IPipelineStep<SamplePipelineContext> NextStep { get; set; }

    public async ValueTask ExecuteAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleGenericStep<T>)} Executed");
        await NextStep.ExecuteAsync(context, cancellationToken);
    }
}
