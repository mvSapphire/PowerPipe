using System.Collections.Generic;
using PowerPipe.Visualization.Antlr.Extensions;
using PowerPipe.Visualization.Mermaid.Graph;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;
using PowerPipe.Visualization.Mermaid.Graph.Nodes;

namespace PowerPipe.Visualization.Antlr;

/// <inheritdoc />
public class PipelineParserVisitor : PipelineParserBaseVisitor<object>
{
    /// <inheritdoc />
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

        pipelineNodes.Add(new AddNode("END OF WORKFLOW"));

        return new Graph(pipelineNodes);
    }

    /// <inheritdoc />
    public override INode VisitAddStep(PipelineParser.AddStepContext context)
    {
        return new AddNode(context.DATA().GetStepName());
    }

    /// <inheritdoc />
    public override INode VisitAddIfStep(PipelineParser.AddIfStepContext context)
    {
        return new AddIfNode(
            context.PREDICATE().GetPredicateName(),
            context.DATA().GetStepName());
    }

    /// <inheritdoc />
    public override INode VisitAddIfElseStep(PipelineParser.AddIfElseStepContext context)
    {
        var (step1, step2) = context.DATA2().GetTwoStepsNames();

        return new AddIfElseNode(
            context.PREDICATE().GetPredicateName(),
            step1,
            step2);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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
