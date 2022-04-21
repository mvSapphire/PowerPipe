using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class LazyStep<TContext, TResult> : IPipelineStep<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly Lazy<IPipelineStep<TContext, TResult>> _step;

    internal LazyStep(Func<IPipelineStep<TContext, TResult>> factory)
    {
        _step = new Lazy<IPipelineStep<TContext, TResult>>(() =>
        {
            var instance = factory();
            instance.NextStep = NextStep;
            return instance;
        });
    }

    public IPipelineStep<TContext, TResult> NextStep { get; set; }

    public async Task ExecuteAsync(TContext context)
    {
        await _step.Value.ExecuteAsync(context);

        await NextStep.ExecuteAsync(context);
    }
}