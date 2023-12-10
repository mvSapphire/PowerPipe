using System;

namespace PowerPipe.Interfaces;

/// <summary>
/// Represents a factory for creating pipeline steps and compensation steps.
/// </summary>
public interface IPipelineStepFactory
{
    /// <summary>
    /// Instance of the service provider
    /// </summary>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Creates an instance of a pipeline step.
    /// </summary>
    /// <typeparam name="TStep">The type of step to create.</typeparam>
    /// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
    /// <returns>An instance of the specified pipeline step.</returns>
    IStepBase<TContext> Create<TStep, TContext>()
        where TStep : IStepBase<TContext>;

    /// <summary>
    /// Creates an instance of a pipeline compensation step.
    /// </summary>
    /// <typeparam name="TStep">The type of compensation step to create.</typeparam>
    /// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
    /// <returns>An instance of the specified pipeline compensation step.</returns>
    IPipelineCompensationStep<TContext> CreateCompensation<TStep, TContext>()
        where TStep : IPipelineCompensationStep<TContext>;
}
