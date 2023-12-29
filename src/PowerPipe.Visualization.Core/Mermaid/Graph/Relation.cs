using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Core.Mermaid.Graph;

public class Relation : IRenderTo<StringBuilder>
{
    public Relation(INode from, INode to, Link link, string text)
    {
        From = from;
        To = to;
        Link = link;
        Text = text;
    }

    public INode From { get; }
    
    public INode To { get; }
    
    public Link Link { get; }
    
    public string Text { get; }

    public void RenderTo(StringBuilder builder)
    {
        builder.Append(From.Id).Append(' ');

        Link.RenderTo(builder);
        builder.Append(' ');

        if (!string.IsNullOrEmpty(Text))
        {
            builder
                .Append("|\"")
                .Append(Text)
                .Append("\"|")
                .Append(' ');
        }
        builder.AppendLine(To.Id);
    }
}
