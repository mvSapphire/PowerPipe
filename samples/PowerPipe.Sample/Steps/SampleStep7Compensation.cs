using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleStep7Compensation : IPipelineCompensationStep<SamplePipelineContext>
{
    public ValueTask CompensateAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleStep7Compensation)} Executed");
        return ValueTask.CompletedTask;
    }
}
