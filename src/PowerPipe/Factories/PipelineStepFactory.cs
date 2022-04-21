using System;
using PowerPipe.Interfaces;

namespace PowerPipe.Factories;

public class PipelineStepFactory : IPipelineStepFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PipelineStepFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IPipelineStep<TContext, TResult> Create<TStep, TContext, TResult>()
        where TStep : IPipelineStep<TContext, TResult>
        where TContext : PipelineContext<TResult>
        where TResult : class
    {
        return _serviceProvider.GetService(typeof(TStep)) as IPipelineStep<TContext, TResult>;
    }
}