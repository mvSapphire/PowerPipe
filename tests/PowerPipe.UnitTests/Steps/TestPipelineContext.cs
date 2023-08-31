namespace PowerPipe.UnitTests.Steps;

public class TestPipelineContext : PipelineContext<TestPipelineResult>
{
    public int Step1RunCount { get; set; }
    
    public override TestPipelineResult GetPipelineResult() => new();
}