using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;
using PowerPipe.Visualization.Core.Mermaid.Graph.Nodes;

namespace PowerPipe.Visualization.Core.Mermaid.Graph;

public class Graph : IGraph
{
    public Graph(List<INode> nodes)
    {
        Nodes = nodes;
        Relations = new List<Relation>();

        LinkNodes(Nodes);
    }

    public List<INode> Nodes { get; }

    public List<Relation> Relations { get; }

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
