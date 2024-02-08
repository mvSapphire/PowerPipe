using System;
using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Mermaid.Graph.Nodes;

/// <summary>
/// Represents a node in a Mermaid graph for an "if-else" condition.
/// </summary>
public class AddIfElseNode : INode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddIfElseNode"/> class.
    /// </summary>
    /// <param name="predicateName">The name of the predicate associated with the "if-else" condition.</param>
    /// <param name="firstChildName">The name of the first child node.</param>
    /// <param name="secondChildName">The name of the second child node.</param>
    public AddIfElseNode(string predicateName, string firstChildName, string secondChildName)
    {
        Id = Guid.NewGuid().ToString("N");
        Shape = Shape.Rhombus;
        PredicateName = predicateName;
        FirstChild = new AddNode(firstChildName);
        SecondChild = new AddNode(secondChildName);
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
    /// Gets the name of the predicate associated with the "if-else" condition.
    /// </summary>
    public string PredicateName { get; }

    /// <summary>
    /// Gets the first child node associated with the "if" branch.
    /// </summary>
    public INode FirstChild { get; }

    /// <summary>
    /// Gets the second child node associated with the "else" branch.
    /// </summary>
    public INode SecondChild { get; }

    /// <inheritdoc />
    public IEnumerable<Relation> LinkTo(INode destination, Link link, string text)
    {
        var relations = new List<Relation>
        {
            new Relation(this, FirstChild, Link.Arrow, "Yes"),
            new Relation(this, SecondChild, Link.Arrow, "No"),
        };

        if (destination is null)
        {
            return relations;
        }

        relations.AddRange(FirstChild.LinkTo(destination, Link.Arrow, string.Empty));
        relations.AddRange(SecondChild.LinkTo(destination, Link.Arrow, string.Empty));

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

        FirstChild.RenderTo(target);
        SecondChild.RenderTo(target);
    }
}
