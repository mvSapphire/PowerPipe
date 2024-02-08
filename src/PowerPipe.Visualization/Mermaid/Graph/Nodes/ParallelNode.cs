using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Mermaid.Graph.Nodes;

/// <summary>
/// Represents a node in a Mermaid graph for parallel processing.
/// </summary>
public class ParallelNode : INode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParallelNode"/> class.
    /// </summary>
    /// <param name="title">The title associated with the parallel node.</param>
    /// <param name="children">The list of child nodes associated with the parallel node.</param>
    public ParallelNode(string title, List<INode> children)
    {
        Id = Guid.NewGuid().ToString("N");
        Title = title;
        Children = children;
    }

    /// <summary>
    /// Gets or sets the unique identifier of the node.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets the shape of the node.
    /// </summary>
    public Shape Shape { get; }

    /// <summary>
    /// Gets the title associated with the parallel node.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets or sets the list of child nodes associated with the parallel node.
    /// </summary>
    public List<INode> Children { get; set; }

    /// <inheritdoc />
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

    /// <inheritdoc />
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
