using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipelineStep<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    public IPipelineStep<TContext, TResult> NextStep { get; set; }

    Task ExecuteAsync(TContext context);
}