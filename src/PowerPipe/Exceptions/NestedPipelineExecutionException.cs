using System;

namespace PowerPipe.Exceptions;

/// <summary>
/// Represents an exception that occurs during nested pipeline execution.
/// </summary>
public class NestedPipelineExecutionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NestedPipelineExecutionException"/> class with a specified exception.
    /// </summary>
    /// <param name="exception">The inner exception that caused the nested pipeline execution to fail.</param>
    public NestedPipelineExecutionException(Exception exception)
        : base("Nested pipeline execution failed", exception)
    {
    }
}
