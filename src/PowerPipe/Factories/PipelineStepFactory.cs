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

    public IPipelineStep<TContext> Create<TStep, TContext>()
        where TStep : IPipelineStep<TContext>
        where TContext : PipelineContext<Type>
    {
        return _serviceProvider.GetService(typeof(TStep)) as IPipelineStep<TContext>;
    }
}