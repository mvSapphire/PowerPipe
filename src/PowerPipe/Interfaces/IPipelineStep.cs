using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipelineStep<TContext>
    where TContext : PipelineContext
{
    public IPipelineStep<TContext> NextStep { get; set; }

    Task ExecuteAsync(TContext context);
}