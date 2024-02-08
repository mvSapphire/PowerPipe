using System;
using PowerPipe.Visualization.Mermaid.Graph.Enum;

namespace PowerPipe.Visualization.Mermaid.Graph.Extensions;

/// <summary>
/// Provides extension methods for rendering the start and end symbols of Mermaid graph shapes.
/// </summary>
public static class ShapeExtensions
{
    /// <summary>
    /// Renders the start symbol for the specified shape.
    /// </summary>
    /// <param name="shape">The shape for which the start symbol is rendered.</param>
    /// <returns>The rendered start symbol as a string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an unsupported shape is provided.</exception>
    public static string RenderStart(this Shape shape)
    {
        return shape switch
        {
            Shape.RoundEdges => "(",
            Shape.Rhombus => "{",
            _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, null)
        };
    }

    /// <summary>
    /// Renders the end symbol for the specified shape.
    /// </summary>
    /// <param name="shape">The shape for which the end symbol is rendered.</param>
    /// <returns>The rendered end symbol as a string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an unsupported shape is provided.</exception>
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
