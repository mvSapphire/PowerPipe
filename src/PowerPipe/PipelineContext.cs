namespace PowerPipe;

/// <summary>
/// Represents the context used in a pipeline execution with an optional result.
/// </summary>
/// <typeparam name="TResult">The type of result returned by the pipeline.</typeparam>
public abstract class PipelineContext<TResult>
    where TResult : class
{
    /// <summary>
    /// Gets the result of the pipeline execution.
    /// </summary>
    /// <returns>The result of the pipeline execution.</returns>
    public abstract TResult GetPipelineResult();
}