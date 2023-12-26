using System.Collections.Generic;
using System.Reflection;

namespace PowerPipe.Visualization.Core.Configurations;

public class PowerPipeVisualizationConfiguration
{
    internal ICollection<Assembly> AssembliesToScan { get; set; } = new List<Assembly>();

    public PowerPipeVisualizationConfiguration ScanFromAssembly(Assembly assembly)
    {
        AssembliesToScan.Add(assembly);

        return this;
    }

    public PowerPipeVisualizationConfiguration ScanFromAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            ScanFromAssembly(assembly);
        }

        return this;
    }
}
