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

    public IStepBase<TContext> Create<TStep, TContext>()
        where TStep : IStepBase<TContext>
    {
        return _serviceProvider.GetService(typeof(TStep)) as IStepBase<TContext>;
    }

    public IPipelineCompensationStep<TContext> CreateCompensation<TStep, TContext>()
        where TStep : IPipelineCompensationStep<TContext>
    {
        return _serviceProvider.GetService(typeof(TStep)) as IPipelineCompensationStep<TContext>;
    }
}