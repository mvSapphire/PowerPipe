using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Core.Mermaid.Graph.Nodes;

public class ParallelNode : INode
{
    public ParallelNode(string title, List<INode> children)
    {
        Id = Guid.NewGuid().ToString("N");
        Title = title;
        Children = children;
    }

    public string Id { get; set; }

    public Shape Shape { get; }

    public string Title { get; }

    public List<INode> Children { get; set; }
    public IEnumerable<Relation> LinkTo(INode destination, Link link, string text)
    {
        if (destination is null)
        {
            return Enumerable.Empty<Relation>();
        }

        var relations = new List<Relation>() { new Relation(this, destination, Link.Arrow, string.Empty) };

        foreach (var child in Children)
        {
            relations.AddRange(child.LinkTo(null, Link.Arrow, string.Empty));
        }

        return relations;
    }

    public void RenderTo(StringBuilder target)
    {
        target
            .Append("subgraph ")
            .Append(Id)
            .Append(' ')
            .Append("[\"")
            .Append(Title)
            .AppendLine("\"]");

        foreach (var child in Children)
        {
            child.RenderTo(target);
        }

        target.AppendLine("end");
    }
}
