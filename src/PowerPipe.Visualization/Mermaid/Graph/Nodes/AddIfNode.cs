using System;
using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Mermaid.Graph.Nodes;

/// <summary>
/// Represents a node in a Mermaid graph for an "if" condition.
/// </summary>
public class AddIfNode : INode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddIfNode"/> class.
    /// </summary>
    /// <param name="predicateName">The name of the predicate associated with the "if" condition.</param>
    /// <param name="childName">The name of the child node.</param>
    public AddIfNode(string predicateName, string childName)
    {
        Id = Guid.NewGuid().ToString("N");
        Shape = Shape.Rhombus;
        PredicateName = predicateName;
        Child = new AddNode(childName);
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
    /// Gets the name of the predicate associated with the "if" condition.
    /// </summary>
    public string PredicateName { get; }

    /// <summary>
    /// Gets the child node associated with the "if" condition.
    /// </summary>
    public INode Child { get; }

    /// <inheritdoc />
    public IEnumerable<Relation> LinkTo(INode destination, Link link, string text)
    {
        var relations = new List<Relation> { new Relation(this, Child, Link.Arrow, "Yes") };

        if (destination is null)
        {
            return relations;
        }

        relations.Add(new Relation(this, destination, Link.Arrow, "No"));
        relations.AddRange(Child.LinkTo(destination, Link.Arrow, string.Empty));

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

        Child.RenderTo(target);
    }
}
