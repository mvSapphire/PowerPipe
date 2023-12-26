using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerPipe.Visualization.Core.Interfaces;
using PowerPipe.Visualization.Core.Models;

namespace PowerPipe.Visualization.Core;

public class MermaidDiagramProvider : IDiagramProvider
{
    private readonly StringBuilder _stringBuilder = new("graph TD\n");

    public string GetDiagram(ICollection<Node> pipelineNodes)
    {
        WritePredicates(pipelineNodes);
        LinkNodes(pipelineNodes);

        return _stringBuilder.ToString();
    }

    private void WritePredicates(IEnumerable<Node> pipelineNodes)
    {
        foreach (var node in pipelineNodes)
        {
            if (!string.IsNullOrEmpty(node.PredicateName))
            {
                _stringBuilder.AppendLine($"{node.PredicateName}{{ {node.PredicateName} }}");
            }

            if (node.Children.Count > 0)
            {
                WritePredicates(node.Children);
            }
        }
    }

    private void LinkNodes(ICollection<Node> pipelineNodes)
    {
        for (var i = 0; i < pipelineNodes.Count; i++)
        {
            var node = pipelineNodes.ElementAt(i);
            var nextNode = i + 1 < pipelineNodes.Count ? pipelineNodes.ElementAt(i + 1) : null;

            switch (node.Type)
            {
                case NodeType.If:
                    LinkIfNode(node, nextNode);
                    break;
                case NodeType.Parallel:
                    LinkParallelNode(node);
                    break;
                case NodeType.Add:
                    LinkAddNode(node, nextNode);
                    break;
                case NodeType.AddIf:
                    LinkAddIfNode(node, nextNode);
                    break;
                case NodeType.AddIfElse:
                    LinkAddIfElseNode(node, nextNode);
                    break;
            }
        }
    }

    private void LinkIfNode(Node node, Node nextNode)
    {
        _stringBuilder.Append($"{node.PredicateName} -->|Yes| ");
        LinkNodes(node.Children);

        if (nextNode != null)
        {
            _stringBuilder.Append($"{node.PredicateName} -->|No| ");
        }
    }

    private void LinkParallelNode(Node node)
    {
        var parallelSubGraphName = $"Parallel{node.Children.Count}";

        _stringBuilder.AppendLine($"subgraph {parallelSubGraphName}");

        foreach (var child in node.Children)
        {
            switch (child.Type)
            {
                case NodeType.Add:
                    _stringBuilder.AppendLine($"{child.Name1} ");
                    continue;
                case NodeType.AddIf:
                    _stringBuilder.AppendLine($"{child.PredicateName} -->|Yes| {child.Name1} ");
                    continue;
                case NodeType.AddIfElse:
                    _stringBuilder.AppendLine($"{child.PredicateName} -->|Yes| {child.Name1}");
                    _stringBuilder.AppendLine($"{child.PredicateName} -->|No| {child.Name2}");
                    continue;
            }
        }

        _stringBuilder.AppendLine("end");
        _stringBuilder.Append($"{parallelSubGraphName} --> ");
    }

    private void LinkAddNode(Node node, Node nextNode)
    {
        if (nextNode != null)
        {
            _stringBuilder.AppendLine($"{node.Name1} --> {nextNode.PredicateName ?? nextNode.Name1}");
        }
        else
        {
            _stringBuilder.AppendLine($"{node.Name1}");
        }
    }

    private void LinkAddIfNode(Node node, Node nextNode)
    {
        if (node.Type == NodeType.AddIf)
        {
            if (nextNode != null)
            {
                _stringBuilder.AppendLine($"{node.PredicateName} -->|Yes| {node.Name1} --> {nextNode.PredicateName ?? nextNode.Name1}");
                _stringBuilder.AppendLine($"{node.PredicateName} -->|No| {nextNode.PredicateName ?? nextNode.Name1}");
            }
            else
            {
                _stringBuilder.AppendLine($"{node.PredicateName} -->|Yes| {node.Name1}");
            }
        }
    }

    private void LinkAddIfElseNode(Node node, Node nextNode)
    {
        if (nextNode != null)
        {
            _stringBuilder.AppendLine($"{node.PredicateName} -->|Yes| {node.Name1} --> {nextNode.PredicateName ?? nextNode.Name1} ");
            _stringBuilder.AppendLine($"{node.PredicateName} -->|No| {node.Name2} --> {nextNode.PredicateName ?? nextNode.Name1} ");
        }
        else
        {
            _stringBuilder.AppendLine($"{node.PredicateName} -->|Yes| {node.Name1}");
            _stringBuilder.AppendLine($"{node.PredicateName} -->|No| {node.Name2}");
        }
    }
}
