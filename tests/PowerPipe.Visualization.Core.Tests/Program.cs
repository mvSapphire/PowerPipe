using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PowerPipe.Sample;
using PowerPipe.Visualization.Core;
using PowerPipe.Visualization.Core.Configurations;

var configuration = Options.Create(new PowerPipeVisualizationConfiguration()
    .ScanFromAssembly(typeof(SamplePipeline).Assembly));

var provider = new MermaidDiagramProvider();

var service = new DiagramsService(configuration, provider, new NullLoggerFactory());

var qq = service.GetDiagrams();

Console.WriteLine(qq.First());

// var input = @"
// new PipelineBuilder<SamplePipelineContext, SamplePipelineResult>(_pipelineStepFactory, context).Parallel((PipelineBuilder<SamplePipelineContext, SamplePipelineResult> b) => b.AddIf<SampleParallelStep1>(SampleParallelStep1ExecutionAllowed).Add<SampleParallelStep2>().Add<SampleParallelStep3>()
// .Add<SampleParallelStep4>()
// .OnError(PipelineStepErrorHandling.Suppress)
// .Add<SampleParallelStep5>()
// .OnError(PipelineStepErrorHandling.Retry)
// .CompensateWith<SampleParallelStep5Compensation>()
// .AddIfElse<SampleParallelStep6, SampleParallelStep7>(SampleParallelStep6ExecutionAllowed)).Add<SampleStep1>().CompensateWith<SampleStep1Compensation>()
// .AddIf<SampleStep2>(Step2ExecutionAllowed)
// .CompensateWith<SampleStep2Compensation>()
// .AddIfElse<SampleStep3, SampleStep4>(Step3ExecutionAllowed)
// .OnError(PipelineStepErrorHandling.Retry)
// .If(NestedPipelineExecutionAllowed, (PipelineBuilder<SamplePipelineContext, SamplePipelineResult> b) => b.Add<SampleStep5>().OnError(PipelineStepErrorHandling.Suppress).Add<SampleStep6>()
//         .OnError(PipelineStepErrorHandling.Retry))
// .Add<SampleStep7>()
// .OnError(PipelineStepErrorHandling.Retry)
// .CompensateWith<SampleStep7Compensation>()
// .Add<SampleGenericStep<int>>();
// ";
//
// var inputStream = new AntlrInputStream(input);
// var pipelineLexer = new PipelineLexer(inputStream);
// var commonTokenStream = new CommonTokenStream(pipelineLexer);
// var pipelineParser = new PipelineParser(commonTokenStream);
//
// var startContext = pipelineParser.start();
//
// var visitor = new PipelineParserVisitor();
// var nodes = (IFlowChart)visitor.Visit(startContext);
//
// Console.WriteLine(nodes.Render());

