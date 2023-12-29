using System;
using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Core.Mermaid.Graph.Nodes;

public class AddIfNode : INode
{
    public AddIfNode(string predicateName, string childName)
    {
        Id = Guid.NewGuid().ToString("N");
        Shape = Shape.Rhombus;
        PredicateName = predicateName;
        Child = new AddNode(childName);
    }

    public string Id { get; }

    public Shape Shape { get; }

    public string PredicateName { get; }

    public INode Child { get; }

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
