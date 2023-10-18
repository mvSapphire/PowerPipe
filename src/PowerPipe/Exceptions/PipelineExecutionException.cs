using System;

namespace PowerPipe.Exceptions;

/// <summary>
/// Represents an exception that occurs during pipeline execution.
/// </summary>
public class PipelineExecutionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineExecutionException"/> class with a specified exception.
    /// </summary>
    /// <param name="exception">The inner exception that caused the pipeline execution to fail.</param>
    public PipelineExecutionException(Exception exception)
        : base("Pipeline execution failed", exception)
    {
    }
}