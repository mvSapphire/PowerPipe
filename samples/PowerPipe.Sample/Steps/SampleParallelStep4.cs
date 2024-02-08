using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Sample.Steps;

public class SampleParallelStep4 : IPipelineParallelStep<SamplePipelineContext>
{
    public ValueTask ExecuteAsync(SamplePipelineContext context, CancellationToken cancellationToken)
    {
        throw new InvalidOperationException();

        // return ValueTask.CompletedTask;
    }
}
