using System;

namespace PowerPipe.Exceptions;

public class PipelineExecutionException : Exception
{
    public PipelineExecutionException(Exception exception)
        : base("Pipeline execution failed", exception)
    {
    }
}