using System;
using System.Text;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;

namespace PowerPipe.Visualization.Core.Mermaid.Graph.Extensions;

public static class LinkExtensions
{
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
