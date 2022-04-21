using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class FinishStep<TContext, TResult> : IPipelineStep<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    public IPipelineStep<TContext, TResult> NextStep { get; set; }

    public Task ExecuteAsync(TContext context)
    {
        return Task.CompletedTask;
    }
}