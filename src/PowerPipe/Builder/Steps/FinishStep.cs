using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class FinishStep : IPipelineStep
{
    public IPipelineStep NextStep { get; set; }

    public Task ExecuteAsync(PipelineContext context)
    {
        return Task.CompletedTask;
    }
}