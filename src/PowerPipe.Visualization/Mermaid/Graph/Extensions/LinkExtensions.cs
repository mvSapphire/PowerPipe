using System;
using System.Text;
using PowerPipe.Visualization.Mermaid.Graph.Enum;

namespace PowerPipe.Visualization.Mermaid.Graph.Extensions;

/// <summary>
/// Provides extension methods for rendering Mermaid graph links.
/// </summary>
public static class LinkExtensions
{
    /// <summary>
    /// Renders the specified link type to the provided <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="link">The link type to render.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> used for rendering.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an unsupported link type is provided.</exception>
    public static void RenderTo(this Link link, StringBuilder builder)
    {
        switch (link)
        {
            case Link.Arrow:
                builder.Append("-->");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(link), link, null);
        }
    }
}
