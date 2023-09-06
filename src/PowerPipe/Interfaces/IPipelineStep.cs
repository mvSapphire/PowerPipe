namespace PowerPipe.Interfaces;

public interface IPipelineStep<TContext> : IStepBase<TContext>
{
    public IPipelineStep<TContext> NextStep { get; set; }
}