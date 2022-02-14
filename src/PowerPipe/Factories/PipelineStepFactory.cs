using System;
using PowerPipe.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PowerPipe.Factories;

public class PipelineStepFactory : IPipelineStepFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PipelineStepFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IPipelineStep Create<TStep>()
        where TStep : IPipelineStep
    {
        return _serviceProvider.GetService<TStep>();
    }
}