using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Extensions;
using PowerPipe.Visualization.Mermaid.Graph.Enum;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Mermaid.Graph;

/// <summary>
/// Represents a relation between two nodes in a Mermaid graph.
/// </summary>
public class Relation : IRenderTo<StringBuilder>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Relation"/> class.
    /// </summary>
    /// <param name="from">The source node of the relation.</param>
    /// <param name="to">The destination node of the relation.</param>
    /// <param name="link">The type of link between the nodes.</param>
    /// <param name="text">The text associated with the relation.</param>
    public Relation(INode from, INode to, Link link, string text)
    {
        From = from;
        To = to;
        Link = link;
        Text = text;
    }

    /// <summary>
    /// Gets the source node of the relation.
    /// </summary>
    public INode From { get; }

    /// <summary>
    /// Gets the destination node of the relation.
    /// </summary>
    public INode To { get; }

    /// <summary>
    /// Gets the type of link between the nodes.
    /// </summary>
    public Link Link { get; }

    /// <summary>
    /// Gets the text associated with the relation.
    /// </summary>
    public string Text { get; }

    /// <inheritdoc />
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
