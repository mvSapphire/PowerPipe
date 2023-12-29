using System;
using PowerPipe.Visualization.Core.Mermaid.Graph.Enum;

namespace PowerPipe.Visualization.Core.Mermaid.Graph.Extensions;

public static class ShapeExtensions
{
    public static string RenderStart(this Shape shape)
    {
        return shape switch
        {
            Shape.RoundEdges => "(",
            Shape.Rhombus => "{",
            _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, null)
        };
    }

    public static string RenderEnd(this Shape shape)
    {
        return shape switch
        {
            Shape.RoundEdges => ")",
            Shape.Rhombus => "}",
            _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, null)
        };
    }
}
