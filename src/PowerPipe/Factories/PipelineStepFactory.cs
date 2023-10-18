using System;
using PowerPipe.Interfaces;

namespace PowerPipe.Factories;

/// <summary>
/// Represents a factory for creating pipeline steps and compensation steps.
/// </summary>
public class PipelineStepFactory : IPipelineStepFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineStepFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve step instances.</param>
    public PipelineStepFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Creates an instance of a pipeline step.
    /// </summary>
    /// <typeparam name="TStep">The type of step to create.</typeparam>
    /// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
    /// <returns>An instance of the specified pipeline step.</returns>
    public IStepBase<TContext> Create<TStep, TContext>()
        where TStep : IStepBase<TContext>
    {
        return _serviceProvider.GetService(typeof(TStep)) as IStepBase<TContext>;
    }

    /// <summary>
    /// Creates an instance of a pipeline compensation step.
    /// </summary>
    /// <typeparam name="TStep">The type of compensation step to create.</typeparam>
    /// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
    /// <returns>An instance of the specified pipeline compensation step.</returns>
    public IPipelineCompensationStep<TContext> CreateCompensation<TStep, TContext>()
        where TStep : IPipelineCompensationStep<TContext>
    {
        return _serviceProvider.GetService(typeof(TStep)) as IPipelineCompensationStep<TContext>;
    }
}