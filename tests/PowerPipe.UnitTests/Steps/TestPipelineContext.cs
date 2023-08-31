namespace PowerPipe.UnitTests.Steps;

public record TestPipelineResult;

public class TestPipelineContext : PipelineContext<TestPipelineResult>
{
    public int Step1RunCount { get; set; }
    
    public override TestPipelineResult GetPipelineResult() => new();
}

public record TestPipelineResult;