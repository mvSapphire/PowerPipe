using System.Collections.Generic;

namespace PowerPipe.Visualization.Core.Models;

public class Node
{
    public NodeType Type { get; set; }

    public string Name1 { get; set; }

    public string Name2 { get; set; }

    public string PredicateName { get; set; }

    public ICollection<Node> Children { get; } = new List<Node>();

    public void AddChild(object source)
    {
        if (source is Node s)
        {
            Children.Add(s);
        }
    }
}
