using System;

namespace PowerPipe.Exceptions;

internal class PipelineExecutionException : Exception
{
    public PipelineExecutionException(Exception exception)
        : base("Pipeline execution failed", exception)
    {
    }
}