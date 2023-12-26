using System.Collections.Generic;
using PowerPipe.Visualization.Core.Models;

namespace PowerPipe.Visualization.Core.Interfaces;

public interface IDiagramProvider
{
    string GetDiagram(ICollection<Node> pipelineNodes);
}
