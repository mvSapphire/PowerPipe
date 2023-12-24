using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerPipe.Visualization.Core.Models;

namespace PowerPipe.Visualization.Core;

public class MermaidConvertor
{
    private readonly StringBuilder _stringBuilder = new StringBuilder("graph TD\n");

    public string ConvertToMermaid(ICollection<Node> pipelineNodes)
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
        for (int i = 0; i < pipelineNodes.Count; i++)
        {
            var node = pipelineNodes.ElementAt(i);
            Node nextNode = null;

            if (i + 1 < pipelineNodes.Count)
            {
                nextNode = pipelineNodes.ElementAt(i + 1);
            }

            if (node.Type is NodeType.If)
            {
                _stringBuilder.Append($"{node.PredicateName} -->|Yes| ");
                LinkNodes(node.Children);
            }

            if (node.Type is NodeType.Parallel)
            {
                var parallelSubGraphName = $"Parallel{node.Children.Count}";
                _stringBuilder.AppendLine($"subgraph {parallelSubGraphName}");
                foreach (var child in node.Children)
                {
                    if (child.Type is NodeType.AddIf)
                    {
                        _stringBuilder.AppendLine($"{child.PredicateName} -->|Yes| {child.Name1} ");
                        continue;
                    }

                    if (child.Type is NodeType.AddIfElse)
                    {
                        _stringBuilder.AppendLine($"{child.PredicateName} -->|Yes| {child.Name1}");
                        _stringBuilder.AppendLine($"{child.PredicateName} -->|No| {child.Name2}");
                        continue;
                    }
                }

                _stringBuilder.AppendLine("end");
                _stringBuilder.Append($"{parallelSubGraphName} --> ");
                continue;
            }

            if (node.Type is NodeType.Add)
            {
                if (nextNode is not null)
                {
                    _stringBuilder.AppendLine($"{node.Name1} --> {nextNode.PredicateName ?? nextNode.Name1}");
                }
                else
                {
                    _stringBuilder.AppendLine($"{node.Name1}");
                }
                
                continue;
            }

            if (node.Type is NodeType.AddIf)
            {
                if (nextNode is not null)
                {
                    _stringBuilder.AppendLine($"{node.PredicateName} -->|Yes| {node.Name1} --> {nextNode.PredicateName ?? nextNode.Name1}");
                    _stringBuilder.AppendLine($"{node.PredicateName} -->|No| {nextNode.PredicateName ?? nextNode.Name1}");
                }
                else
                {
                    _stringBuilder.AppendLine($"{node.PredicateName} -->|Yes| {node.Name1}");
                }
                continue;
            }

            if (node.Type is NodeType.AddIfElse)
            {
                if(nextNode is not null)
                {
                    _stringBuilder.AppendLine($"{node.PredicateName} -->|Yes| {node.Name1} --> {nextNode.PredicateName ?? nextNode.Name1} ");
                    _stringBuilder.AppendLine($"{node.PredicateName} -->|No| {node.Name2} --> {nextNode.PredicateName ?? nextNode.Name1} ");
                }
                else
                {
                    _stringBuilder.AppendLine($"{node.PredicateName} -->|Yes| {node.Name1} --> ");
                    _stringBuilder.AppendLine($"{node.PredicateName} -->|No| {node.Name2} --> ");
                }
                continue;
            }
        }
    }
}
