using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Enum;

namespace PowerPipe.Visualization.Mermaid.Graph.Interfaces;

/// <summary>
/// Represents an interface for a node in a Mermaid graph.
/// </summary>
public interface INode : IRenderTo<StringBuilder>
{
    /// <summary>
    /// Gets the identifier of the node.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the shape of the node.
    /// </summary>
    Shape Shape { get; }

    /// <summary>
    /// Creates and returns relations from the current node to a destination node.
    /// </summary>
    /// <param name="destination">The destination node.</param>
    /// <param name="link">The type of link to create.</param>
    /// <param name="text">The text associated with the link.</param>
    /// <returns>An enumerable collection of relations created.</returns>
    IEnumerable<Relation> LinkTo(INode destination, Link link, string text);
}
