namespace PowerPipe.Interfaces;

public interface IPipelineStepFactory
{
    IPipelineStep<TContext, TResult> Create<TStep, TContext, TResult>()
        where TStep : IPipelineStep<TContext, TResult>
        where TContext : PipelineContext<TResult>
        where TResult : class;
}