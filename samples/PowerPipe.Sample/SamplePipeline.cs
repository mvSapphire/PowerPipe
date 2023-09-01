using PowerPipe.Builder;
using PowerPipe.Builder.Steps;
using PowerPipe.Interfaces;
using PowerPipe.Sample.Steps;

namespace PowerPipe.Sample;

public class SamplePipeline
{
    private readonly IPipelineStepFactory _pipelineStepFactory;

    private bool Step2ExecutionAllowed(SamplePipelineContext context) => true;
    private bool Step3ExecutionAllowed(SamplePipelineContext context) => false;
    private bool NestedPipelineExecutionAllowed(SamplePipelineContext context) => true;

    public SamplePipeline(IPipelineStepFactory pipelineStepFactory)
    {
        _pipelineStepFactory = pipelineStepFactory;
    }

    public IPipeline<SamplePipelineResult> SetupPipeline()
    {
        var context = new SamplePipelineContext();

        var builder = new PipelineBuilder<SamplePipelineContext, SamplePipelineResult>(_pipelineStepFactory, context)
            .Add<SampleStep1>()
                .CompensateWith<SampleStep1Compensation>()
            .AddIf<SampleStep2>(Step2ExecutionAllowed)
                .CompensateWith<SampleStep2Compensation>()
            .AddIfElse<SampleStep3, SampleStep4>(Step3ExecutionAllowed)
                .OnError(PipelineStepErrorHandling.Retry)
            .If(NestedPipelineExecutionAllowed, b => b
                .Add<SampleStep5>()
                    .OnError(PipelineStepErrorHandling.Suppress)
                .Add<SampleStep6>()
                    .OnError(PipelineStepErrorHandling.Retry))
            .Add<SampleStep7>()
                .OnError(PipelineStepErrorHandling.Retry)
                .CompensateWith<SampleStep7Compensation>();

        return builder.Build();
    }
}

public record SamplePipelineResult;

public class SamplePipelineContext : PipelineContext<SamplePipelineResult>
{
    public override SamplePipelineResult GetPipelineResult() => null;
};

