using System.Collections.Generic;
using PowerPipe.Visualization.Core.Mermaid.Graph;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;
using PowerPipe.Visualization.Core.Mermaid.Graph.Nodes;

namespace PowerPipe.Visualization.Core.Antlr;

public class PipelineParserVisitor : PipelineParserBaseVisitor<object>
{
    public override IGraph VisitStart(PipelineParser.StartContext context)
    {
        var pipelineNodes = new List<INode>();

        foreach (var child in context.children)
        {
            var node = Visit(child);

            if (node is INode n)
            {
                pipelineNodes.Add(n);
            }
        }

        return new Graph(pipelineNodes);
    }

    public override INode VisitAddStep(PipelineParser.AddStepContext context)
    {
        return new AddNode(context.DATA().GetText());
    }

    public override INode VisitAddIfStep(PipelineParser.AddIfStepContext context)
    {
        return new AddIfNode(context.PREDICATE().GetText().TrimStart('(').TrimEnd(')'), context.DATA().GetText());
    }

    public override INode VisitAddIfElseStep(PipelineParser.AddIfElseStepContext context)
    {
        return new AddIfElseNode(context.PREDICATE().GetText().TrimStart('(').TrimEnd(')'), context.DATA()[0].GetText(), context.DATA()[1].GetText());
    }

    public override INode VisitIfStep(PipelineParser.IfStepContext context)
    {
        var children = new List<INode>();

        foreach (var child in context.children)
        {
            var parsedChild = Visit(child);

            if (parsedChild is INode c)
            {
                children.Add(c);
            }
        }

        return new IfNode(context.DATA().GetText(), children);
    }

    public override INode VisitParallelStep(PipelineParser.ParallelStepContext context)
    {
        var children = new List<INode>();

        foreach (var child in context.children)
        {
            var parsedChild = Visit(child);

            if (parsedChild is INode c)
            {
                children.Add(c);
            }
        }

        return new ParallelNode("Parallel", children);
    }
}
