using System.Collections.Generic;
using PowerPipe.Visualization.Core.Antlr.Extensions;
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

        pipelineNodes.Add(new AddNode("END OF PIPELINE"));

        return new Graph(pipelineNodes);
    }

    public override INode VisitAddStep(PipelineParser.AddStepContext context)
    {
        return new AddNode(context.DATA().GetStepName());
    }

    public override INode VisitAddIfStep(PipelineParser.AddIfStepContext context)
    {
        return new AddIfNode(
            context.PREDICATE().GetPredicateName(),
            context.DATA().GetStepName());
    }

    public override INode VisitAddIfElseStep(PipelineParser.AddIfElseStepContext context)
    {
        var (step1, step2) = context.DATA2().GetTwoStepsNames();

        return new AddIfElseNode(
            context.PREDICATE().GetPredicateName(),
            step1,
            step2);
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

        return new IfNode(context.OPENPREDICATE().GetOpenPredicateName(), children);
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
