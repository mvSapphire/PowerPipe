using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Mermaid.Graph.Nodes;

/// <summary>
/// Represents a generic node in a Mermaid graph.
/// </summary>
public class AddNode : INode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddNode"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the node.</param>
    public AddNode(string name)
    {
        Id = Guid.NewGuid().ToString("N");
        Name = name;
        Shape = Shape.RoundEdges;
    }

    /// <summary>
    /// Gets the unique identifier of the node.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the shape of the node.
    /// </summary>
    public Shape Shape { get; }

    /// <summary>
    /// Gets the name of the node.
    /// </summary>
    public string Name { get; }

    /// <inheritdoc />
    public IEnumerable<Relation> LinkTo(INode destination, Link link, string text)
    {
        return destination is null ? Enumerable.Empty<Relation>() : new[] { new Relation(this, destination, Link.Arrow, string.Empty) };
    }

    /// <inheritdoc />
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
