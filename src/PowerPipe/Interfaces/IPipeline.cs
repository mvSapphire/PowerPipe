using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipeline
{
    Task<PipelineResult> RunAsync(bool returnResult = true);
}