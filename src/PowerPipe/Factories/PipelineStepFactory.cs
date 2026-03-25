using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PowerPipe.Interfaces;

namespace PowerPipe.Factories;

/// <inheritdoc/>
public class PipelineStepFactory : IPipelineStepFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineStepFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve step instances.</param>
    public PipelineStepFactory(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public IStepBase<TContext> Create<TStep, TContext>()
        where TStep : IStepBase<TContext>
    {
        return _serviceProvider.GetRequiredService(typeof(TStep)) as IStepBase<TContext>;
    }

    /// <inheritdoc/>
    public IPipelineCompensationStep<TContext> CreateCompensation<TStep, TContext>()
        where TStep : IPipelineCompensationStep<TContext>
    {
        return _serviceProvider.GetRequiredService(typeof(TStep)) as IPipelineCompensationStep<TContext>;
    }

    /// <inheritdoc/>
    public ILoggerFactory GetLoggerFactory()
    {
        return _serviceProvider.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
    }
}
