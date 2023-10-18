namespace PowerPipe.Interfaces;

/// <summary>
/// Represents a parallel execution step in a pipeline.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
public interface IPipelineParallelStep<in TContext> : IStepBase<TContext>
{
}