namespace PowerPipe.UnitTests.Steps;

public record TestPipelineResult;

public class TestPipelineContext : PipelineContext<TestPipelineResult>
{
    public int Step1RunCount { get; set; }

    public int ParallelStepRunCount { get; set; }

    public int CompensationStepRunCount { get; set; }

    public override TestPipelineResult GetPipelineResult() => new();
}