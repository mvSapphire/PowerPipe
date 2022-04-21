using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class FinishStep<TContext> : IPipelineStep<TContext>
{
    public IPipelineStep<TContext> NextStep { get; set; }

    public Task ExecuteAsync(TContext context)
    {
        return Task.CompletedTask;
    }
}