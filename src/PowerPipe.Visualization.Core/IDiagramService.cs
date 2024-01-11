using System.Collections.Generic;

namespace PowerPipe.Visualization.Core;

public interface IDiagramService
{
    ICollection<string> GetDiagrams();
}
