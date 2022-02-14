using System;
using EasyPipe.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPipe.Factories;

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