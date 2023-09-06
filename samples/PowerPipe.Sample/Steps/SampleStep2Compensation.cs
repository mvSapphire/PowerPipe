using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleStep2Compensation : IPipelineCompensationStep<SamplePipelineContext>
{
    public Task CompensateAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleStep2Compensation)} Executed");
        return Task.CompletedTask;
    }
}