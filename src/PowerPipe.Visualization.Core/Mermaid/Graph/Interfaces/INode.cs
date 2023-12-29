using System.Collections.Generic;
using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;

namespace PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

public interface INode : IRenderTo<StringBuilder>
{
    string Id { get; }

    Shape Shape { get; }

    IEnumerable<Relation> LinkTo(INode destination, Link link, string text);
}
