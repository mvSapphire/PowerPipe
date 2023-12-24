using System.Collections.Generic;
using PowerPipe.Visualization.Core.Models;

namespace PowerPipe.Visualization.Core.Antlr;

public class PipelineParserVisitor : PipelineParserBaseVisitor<object>
{
    public override ICollection<Node> VisitStart(PipelineParser.StartContext context)
    {
        var pipelineNodes = new List<Node>();

        foreach (var child in context.children)
        {
            var node = Visit(child);

            if (node is Node n)
            {
                pipelineNodes.Add(n);
            }
        }

        return pipelineNodes;
    }

    public override Node VisitAddStep(PipelineParser.AddStepContext context)
    {
        return new Node { Type = NodeType.Add, Name1 = context.DATA().GetText() };
    }

    public override Node VisitAddIfStep(PipelineParser.AddIfStepContext context)
    {
        return new Node { Type = NodeType.AddIf, Name1 = context.DATA().GetText(), PredicateName = context.PREDICATE().GetText().TrimStart('(').TrimEnd(')')};
    }

    public override Node VisitAddIfElseStep(PipelineParser.AddIfElseStepContext context)
    {
        return new Node
        {
            Type = NodeType.AddIfElse,
            Name1 = context.DATA()[0].GetText(),
            Name2 = context.DATA()[1].GetText(),
            PredicateName = context.PREDICATE().GetText().TrimStart('(').TrimEnd(')')
        };
    }

    public override Node VisitIfStep(PipelineParser.IfStepContext context)
    {
        var node = new Node { Type = NodeType.If, Name1 = "If", PredicateName = context.DATA().GetText()};

        foreach (var child in context.children)
        {
            node.AddChild(Visit(child));
        }

        return node;
    }

    public override Node VisitParallelStep(PipelineParser.ParallelStepContext context)
    {
        var node = new Node { Type = NodeType.Parallel, Name1 = "Parallel", };

        foreach (var child in context.children)
        {
            node.AddChild(Visit(child));
        }

        return node;
    }
}
