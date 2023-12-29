using Antlr4.Runtime;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PowerPipe.Sample;
using PowerPipe.Visualization.Core;
using PowerPipe.Visualization.Core.Antlr;
using PowerPipe.Visualization.Core.Configurations;
using PowerPipe.Visualization.Core.Mermaid.Graph;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;
using PowerPipe.Visualization.Core.Mermaid.Graph.Nodes;

var configuration = Options.Create(new PowerPipeVisualizationConfiguration()
    .ScanFromAssembly(typeof(SamplePipeline).Assembly));

var service = new DiagramsService(configuration, new NullLoggerFactory());

var qq = service.GetDiagrams();

Console.WriteLine(qq.First());

// var input = @"
// new PipelineBuilder<SamplePipelineContext, SamplePipelineResult>(_pipelineStepFactory, context).Parallel((PipelineBuilder<SamplePipelineContext, SamplePipelineResult> b) => b
// .AddIf<SampleParallelStep1>(SampleParallelStep1ExecutionAllowed)
// .Add<SampleParallelStep2>()
// .Add<SampleParallelStep3>()
// .Add<SampleParallelStep4>()
// .OnError(PipelineStepErrorHandling.Suppress)
// .Add<SampleParallelStep5>()
// .OnError(PipelineStepErrorHandling.Retry)
// .CompensateWith<SampleParallelStep5Compensation>()
// .AddIfElse<SampleParallelStep6, SampleParallelStep7>(SampleParallelStep6ExecutionAllowed))
//
// .Add<SampleStep1>().CompensateWith<SampleStep1Compensation>()
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
// var nodes = (IGraph)visitor.Visit(startContext);
//
// Console.WriteLine(nodes.Render());


// var nodes = new List<INode>()
// {
//     new ParallelNode("Parallel", new List<INode>()
//     {
//         new AddNode("ParallelAdd"),
//         new AddIfNode("ParallelAddIfPredicate", "ParallelAddIf"),
//         new AddIfElseNode("ParallelAddIfElsePredicate", "ParallelAddIfElse1", "ParallelAddIfElse2"),
//     }),
//     new AddNode("Add"),
//     new AddIfNode("AddIfPredicate", "AddIf"),
//     new AddIfElseNode("AddIfElsePredicate", "AddIfElse1", "AddIfElse2"),
//     new IfNode("IfPredicate", new List<INode>()
//     {
//         new AddNode("IfAdd"),
//         new AddIfNode("IfAddIfPredicate", "IfAddIf"),
//         new AddIfElseNode("IfAddIfElsePredicate", "IfAddIfElse1", "IfAddIfElse2"),
//         new AddNode("IfAdd2"),
//     }),
//     new AddNode("Add2"),
// };
//
// var graph = new Graph(nodes);
//
// Console.WriteLine(graph.Render());
