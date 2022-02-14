namespace PowerPipe.Interfaces;

public interface IPipelineStepFactory
{
    IPipelineStep Create<TStep>() where TStep : IPipelineStep;
}