namespace PowerPipe.Visualization.Mermaid.Graph.Interfaces;

/// <summary>
/// Represents an interface for rendering an object to a specified target.
/// </summary>
/// <typeparam name="T">The type of target to render to.</typeparam>
public interface IRenderTo<in T>
{
    /// <summary>
    /// Renders the object to the specified target.
    /// </summary>
    /// <param name="target">The target to render the object to.</param>
    void RenderTo(T target);
}
