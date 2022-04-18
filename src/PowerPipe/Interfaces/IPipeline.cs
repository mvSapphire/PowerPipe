using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipeline<TContext>
    where TContext : PipelineContext
{
    Task<PipelineResult> RunAsync(bool returnResult = true);
}