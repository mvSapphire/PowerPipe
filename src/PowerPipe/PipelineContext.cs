namespace PowerPipe;

public abstract class PipelineContext<TResult>
    where TResult : class
{
    public abstract TResult GetPipelineResult();
}