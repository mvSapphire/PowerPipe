using System;

namespace PowerPipe.Exceptions;

/// <summary>
/// Represents an exception that occurs during pipeline execution.
/// </summary>
public class PipelineExecutionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineExecutionException"/> class.
    /// </summary>
    public PipelineExecutionException()
        : base("Pipeline execution failed")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineExecutionException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PipelineExecutionException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineExecutionException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public PipelineExecutionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineExecutionException"/> class with a specified exception.
    /// </summary>
    /// <param name="exception">The inner exception that caused the pipeline execution to fail.</param>
    public PipelineExecutionException(Exception exception)
        : base("Pipeline execution failed", exception)
    {
    }
}