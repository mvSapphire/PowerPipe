using System.Threading.Tasks;

namespace EasyPipe.Interfaces;

public interface IPipeline
{
    Task<PipelineResult> RunAsync(bool returnResult = true);
}