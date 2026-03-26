using System;

namespace PowerPipe.Exceptions;

/// <summary>
/// Represents an exception that occurs during nested pipeline execution.
/// </summary>
public class NestedPipelineExecutionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NestedPipelineExecutionException"/> class.
    /// </summary>
    public NestedPipelineExecutionException()
        : base("Nested pipeline execution failed")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NestedPipelineExecutionException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public NestedPipelineExecutionException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NestedPipelineExecutionException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NestedPipelineExecutionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NestedPipelineExecutionException"/> class with a specified exception.
    /// </summary>
    /// <param name="exception">The inner exception that caused the nested pipeline execution to fail.</param>
    public NestedPipelineExecutionException(Exception exception)
        : base("Nested pipeline execution failed", exception)
    {
    }
}
