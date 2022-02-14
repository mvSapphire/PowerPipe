using System.Threading.Tasks;

namespace EasyPipe.Interfaces;

public interface IPipelineStep
{
    public IPipelineStep NextStep { get; set; }

    Task ExecuteAsync(PipelineContext context);
}