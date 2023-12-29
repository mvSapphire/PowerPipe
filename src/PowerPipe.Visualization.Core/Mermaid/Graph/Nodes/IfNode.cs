using System;
using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Core.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Core.Mermaid.Graph.Nodes;

public class IfNode : INode
{
    public IfNode(string predicateName, List<INode> children)
    {
        Id = Guid.NewGuid().ToString("N");
        Shape = Shape.Rhombus;
        PredicateName = predicateName;
        Children = children;
    }

    public string Id { get; set; }

    public Shape Shape { get; }

    public string PredicateName { get; set; }

    public List<INode> Children { get; set; }

    public IEnumerable<Relation> LinkTo(INode destination, Link link, string text)
    {
        var relations = new List<Relation> { new Relation(this, Children[0], Link.Arrow, "Yes") };

        if (destination is null)
        {
            return relations;
        }

        relations.Add(new Relation(this, destination, Link.Arrow, "No"));
        relations.AddRange(Children[^1].LinkTo(destination, Link.Arrow, string.Empty));

        return relations;
    }

    public void RenderTo(StringBuilder target)
    {
        target
            .Append(Id)
            .Append(Shape.RenderStart())
            .Append('"')
            .Append(PredicateName)
            .Append('"')
            .AppendLine(Shape.RenderEnd());

        foreach (var child in Children)
        {
            child.RenderTo(target);
        }
    }
}
