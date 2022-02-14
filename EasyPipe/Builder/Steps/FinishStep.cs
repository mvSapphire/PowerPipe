using System.Threading.Tasks;
using EasyPipe.Interfaces;

namespace EasyPipe.Builder.Steps;

public class FinishStep : IPipelineStep
{
    public IPipelineStep NextStep { get; set; }

    public Task ExecuteAsync(PipelineContext context)
    {
        return Task.CompletedTask;
    }
}