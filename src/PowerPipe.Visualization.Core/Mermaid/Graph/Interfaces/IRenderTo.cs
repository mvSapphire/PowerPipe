namespace PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

public interface IRenderTo<in T>
{
    void RenderTo(T target);
}
