using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleParallelStep5 : IPipelineParallelStep<SamplePipelineContext>
{
    public Task ExecuteAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleParallelStep5)} Executed");

        // throw new InvalidOperationException();

        return Task.CompletedTask;
    }
}