using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleStep2Compensation : IPipelineCompensationStep<SamplePipelineContext>
{
    public ValueTask CompensateAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleStep2Compensation)} Executed");
        return ValueTask.CompletedTask;
    }
}
