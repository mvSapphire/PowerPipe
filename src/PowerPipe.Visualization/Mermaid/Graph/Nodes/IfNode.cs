using System;
using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Mermaid.Graph.Nodes;

/// <summary>
/// Represents a node in a Mermaid graph for an "if" condition with multiple children.
/// </summary>
public class IfNode : INode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IfNode"/> class.
    /// </summary>
    /// <param name="predicateName">The name of the predicate associated with the "if" condition.</param>
    /// <param name="children">The list of child nodes associated with the "if" condition.</param>
    public IfNode(string predicateName, List<INode> children)
    {
        Id = Guid.NewGuid().ToString("N");
        Shape = Shape.Rhombus;
        PredicateName = predicateName;
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
    /// Gets or sets the name of the predicate associated with the "if" condition.
    /// </summary>
    public string PredicateName { get; set; }

    /// <summary>
    /// Gets or sets the list of child nodes associated with the "if" condition.
    /// </summary>
    public List<INode> Children { get; set; }

    /// <inheritdoc />
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

    /// <inheritdoc />
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
