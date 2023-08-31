namespace PowerPipe.Interfaces;

public interface IPipelineStepFactory
{
    IPipelineStep<TContext> Create<TStep, TContext>()
        where TStep : IPipelineStep<TContext>;

    IPipelineCompensationStep<TContext> CreateCompensation<TStep, TContext>()
        where TStep : IPipelineCompensationStep<TContext>;
}