using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;
using PowerPipe.Visualization.Mermaid.Graph.Nodes;

namespace PowerPipe.Visualization.Mermaid.Graph;

/// <summary>
/// Represents a Mermaid graph with nodes and relations.
/// </summary>
public class Graph : IGraph
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Graph"/> class with the specified nodes.
    /// </summary>
    /// <param name="nodes">The list of nodes in the graph.</param>
    public Graph(List<INode> nodes)
    {
        Nodes = nodes;
        Relations = new List<Relation>();

        LinkNodes(Nodes);
    }

    /// <summary>
    /// Gets the list of nodes in the graph.
    /// </summary>
    public List<INode> Nodes { get; }

    /// <summary>
    /// Gets the list of relations in the graph.
    /// </summary>
    public List<Relation> Relations { get; }

    /// <inheritdoc />
    public string Render()
    {
        var builder = new StringBuilder();
        builder.AppendLine("graph TB");

        foreach (var node in Nodes)
            node.RenderTo(builder);

        foreach (var relation in Relations)
            relation.RenderTo(builder);

        return builder.ToString();
    }

    private void LinkNodes(List<INode> nodes, bool linkInternal = true)
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            var currentNode = nodes[i];
            var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

            if(linkInternal)
                Relations.AddRange(currentNode.LinkTo(nextNode, Link.Arrow, string.Empty));
            
            switch (currentNode)
            {
                case IfNode ifNode:
                    LinkNodes(ifNode.Children);
                    continue;
                case ParallelNode parallelNode:
                    LinkNodes(parallelNode.Children, false);
                    continue;
            }
        }
    }
}
