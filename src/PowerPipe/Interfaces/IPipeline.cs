using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipeline<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    Task<TResult> RunAsync(bool returnResult = true);
}