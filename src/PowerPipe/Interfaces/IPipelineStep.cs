namespace PowerPipe.Interfaces;

/// <summary>
/// Represents a step in a pipeline with the ability to specify the next step.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
public interface IPipelineStep<TContext> : IStepBase<TContext>
{
    /// <summary>
    /// Gets or sets the next step in the pipeline.
    /// This property is managed by the pipeline infrastructure and should not be set by consumer code.
    /// </summary>
    public IPipelineStep<TContext> NextStep { get; set; }
}