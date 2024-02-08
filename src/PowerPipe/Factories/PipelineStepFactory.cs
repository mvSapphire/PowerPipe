using System;
using PowerPipe.Interfaces;

namespace PowerPipe.Factories;

/// <inheritdoc/>
public class PipelineStepFactory : IPipelineStepFactory
{
    /// <inheritdoc/>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineStepFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve step instances.</param>
    public PipelineStepFactory(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public IStepBase<TContext> Create<TStep, TContext>()
        where TStep : IStepBase<TContext>
    {
        return ServiceProvider.GetService(typeof(TStep)) as IStepBase<TContext>;
    }

    /// <inheritdoc/>
    public IPipelineCompensationStep<TContext> CreateCompensation<TStep, TContext>()
        where TStep : IPipelineCompensationStep<TContext>
    {
        return ServiceProvider.GetService(typeof(TStep)) as IPipelineCompensationStep<TContext>;
    }
}
