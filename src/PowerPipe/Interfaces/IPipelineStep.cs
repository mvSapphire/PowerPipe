using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipelineStep
{
    public IPipelineStep NextStep { get; set; }

    Task ExecuteAsync(PipelineContext context);
}