namespace PowerPipe.Interfaces;

public interface IPipelineStepFactory
{
    IStepBase<TContext> Create<TStep, TContext>()
        where TStep : IStepBase<TContext>;

    IPipelineCompensationStep<TContext> CreateCompensation<TStep, TContext>()
        where TStep : IPipelineCompensationStep<TContext>;
}