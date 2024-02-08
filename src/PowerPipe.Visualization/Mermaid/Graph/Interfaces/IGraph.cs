namespace PowerPipe.Visualization.Mermaid.Graph.Interfaces;

/// <summary>
/// Represents an interface for rendering Mermaid graphs.
/// </summary>
public interface IGraph
{
    /// <summary>
    /// Renders the Mermaid graph as a string.
    /// </summary>
    /// <returns>The rendered Mermaid graph as a string.</returns>
    string Render();
}
