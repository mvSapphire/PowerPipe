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
            .Parallel(b => b
                .AddIf<SampleParallelStep1>(_ => false)
                .Add<SampleParallelStep2>()
                .Add<SampleParallelStep3>()
                .Add<SampleParallelStep4>()
                    .OnError(PipelineStepErrorHandling.Suppress)
                .Add<SampleParallelStep5>()
                    .OnError(PipelineStepErrorHandling.Retry)
                    .CompensateWith<SampleParallelStep5Compensation>()
                .AddIfElse<SampleParallelStep6, SampleParallelStep7>(_ => false))
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
                .CompensateWith<SampleStep7Compensation>()
            .Add<SampleGenericStep<int>>();

        return builder.Build();
    }
}

public record SamplePipelineResult;

public class SamplePipelineContext : PipelineContext<SamplePipelineResult>
{
    public int Test1 { get; set; }

    public override SamplePipelineResult GetPipelineResult() => null;
}

