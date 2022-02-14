using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class LazyStep : IPipelineStep
{
    private readonly Lazy<IPipelineStep> _step;

    internal LazyStep(Func<IPipelineStep> factory)
    {
        _step = new Lazy<IPipelineStep>(() =>
        {
            var instance = factory();
            instance.NextStep = NextStep;
            return instance;
        });
    }
    
    public IPipelineStep NextStep { get; set; }

    public async Task ExecuteAsync(PipelineContext context)
    {
        await _step.Value.ExecuteAsync(context);
    }
}