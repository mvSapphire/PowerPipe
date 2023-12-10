using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleParallelStep3 : IPipelineParallelStep<SamplePipelineContext>
{
    public ValueTask ExecuteAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(SampleParallelStep3)} Executed");
        return ValueTask.CompletedTask;
    }
}
