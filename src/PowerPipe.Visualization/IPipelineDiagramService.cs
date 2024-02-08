using System.Collections.Generic;

namespace PowerPipe.Visualization;


/// <summary>
/// Represents a service for creating diagrams.
/// </summary>
public interface IPipelineDiagramService
{
    /// <summary>
    /// Parses diagrams to mermaid.
    /// </summary>
    /// <returns>Dictionary of diagrams where the Key is the name of the file where the diagram was found.</returns>
    IDictionary<string, string> GetDiagrams();
}
