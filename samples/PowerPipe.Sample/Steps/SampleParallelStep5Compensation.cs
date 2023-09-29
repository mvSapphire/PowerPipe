using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleParallelStep5Compensation : IPipelineCompensationStep<SamplePipelineContext>
{
    public Task CompensateAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleParallelStep5Compensation)} Executed");
        return Task.CompletedTask;
    }
}