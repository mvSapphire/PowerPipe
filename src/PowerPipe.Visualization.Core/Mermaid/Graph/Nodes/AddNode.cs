using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Core.Mermaid.Graph.Nodes;

public class AddNode : INode
{
    public AddNode(string name)
    {
        Id = Guid.NewGuid().ToString("N");
        Name = name;
        Shape = Shape.RoundEdges;
    }

    public string Id { get; }

    public Shape Shape { get; }

    public string Name { get; }

    public IEnumerable<Relation> LinkTo(INode destination, Link link, string text)
    {
        return destination is null ? Enumerable.Empty<Relation>() : new[] { new Relation(this, destination, Link.Arrow, string.Empty) };
    }

    public void RenderTo(StringBuilder target)
    {
        target
            .Append(Id)
            .Append(Shape.RenderStart())
            .Append('"')
            .Append(Name)
            .Append('"')
            .AppendLine(Shape.RenderEnd());
    }
}
