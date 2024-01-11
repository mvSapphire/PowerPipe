using Antlr4.Runtime;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PowerPipe.Sample;
using PowerPipe.Visualization.Core;
using PowerPipe.Visualization.Core.Antlr;
using PowerPipe.Visualization.Core.Configurations;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

var configuration = Options.Create(new PowerPipeVisualizationConfiguration()
    .ScanFromAssembly(typeof(SamplePipeline).Assembly));

var service = new DiagramsService(configuration, new NullLoggerFactory());

var qq = service.GetDiagrams();

Console.WriteLine(qq.First());

// var input = @"
// new PipelineBuilder<ChargebackProcessingContext, ChargebackWorkflowExecuteResult>(pipelineStepFactory, context).If(IsInsert, (PipelineBuilder<ChargebackProcessingContext, ChargebackWorkflowExecuteResult> b) => b.Add<CashierDataConnectorHandlerStep>().If(CashierParametersExist, (PipelineBuilder<ChargebackProcessingContext, ChargebackWorkflowExecuteResult> b) => b.AddIf<AddPanTokenInBlacklistHandlerStep>(IsPanTokenExists).AddIf<AddWalletInBlacklistHandlerStep>(IsWalletExists).AddIf<AddPhoneInBlackListHandlerStep>(IsPhoneExists)
//                         .AddIf<AddEmailInBlackListHandlerStep>(IsEmailExists))).AddIf<BlacklistRemoveByExtendedDataHandlerStep>(IsDelete).Build();
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
