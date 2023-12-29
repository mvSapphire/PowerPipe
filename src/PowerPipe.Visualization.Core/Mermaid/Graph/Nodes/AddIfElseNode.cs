using System;
using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Core.Mermaid.Graph.Nodes;

public class AddIfElseNode : INode
{
    public AddIfElseNode(string predicateName, string firstChildName, string secondChildName)
    {
        Id = Guid.NewGuid().ToString("N");
        Shape = Shape.Rhombus;
        PredicateName = predicateName;
        FirstChild = new AddNode(firstChildName);
        SecondChild = new AddNode(secondChildName);
    }

    public string Id { get; }

    public Shape Shape { get; }

    public string PredicateName { get; }

    public INode FirstChild { get; }

    public INode SecondChild { get; }

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
