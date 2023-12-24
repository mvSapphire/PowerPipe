using Antlr4.Runtime;
using PowerPipe.Visualization.Core;
using PowerPipe.Visualization.Core.Antlr;
using PowerPipe.Visualization.Core.Models;

var input = @"
new PipelineBuilder<SamplePipelineContext, SamplePipelineResult>(_pipelineStepFactory, context)
.Parallel((PipelineBuilder<SamplePipelineContext, SamplePipelineResult> b) => b
      .AddIf<SampleParallelStep1>(predicate1)
      .Add<SampleParallelStep2>()
      .AddIfElse<SampleParallelStep6, SampleParallelStep7>(predicate2))
.Add<SampleStep1>().CompensateWith<SampleStep1Compensation>()
.AddIf<SampleStep2>(Step2ExecutionAllowed)
.CompensateWith<SampleStep2Compensation>()
.AddIfElse<SampleStep3, SampleStep4>(Step3ExecutionAllowed)
.OnError(PipelineStepErrorHandling.Retry)
.If(NestedPipelineExecutionAllowed, (PipelineBuilder<SamplePipelineContext, SamplePipelineResult> b) => b
    .Add<SampleStep6>()
    .Add<SampleStep7>()
    .AddIf<SampleStep23>(Step23ExecutionAllowed)
    .AddIfElse<SampleStep333, SampleStep444>(Step3333ExecutionAllowed)
.OnError(PipelineStepErrorHandling.Retry)
.CompensateWith<SampleStep7Compensation>();
";
var inputStream = new AntlrInputStream(input);
var pipelineLexer = new PipelineLexer(inputStream);
var commonTokenStream = new CommonTokenStream(pipelineLexer);
var pipelineParser = new PipelineParser(commonTokenStream);

var startContext = pipelineParser.start();

var visitor = new PipelineParserVisitor();
var res = (ICollection<Node>)visitor.Visit(startContext);

var con = new MermaidConvertor();

Console.WriteLine(con.ConvertToMermaid(res));

Console.WriteLine();
