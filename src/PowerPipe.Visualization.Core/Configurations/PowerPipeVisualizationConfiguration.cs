using System;
using System.Collections.Generic;
using System.Reflection;

namespace PowerPipe.Visualization.Core.Configurations;

public class PowerPipeVisualizationConfiguration
{
    internal ICollection<Assembly> AssembliesToScan { get; set; } = new List<Assembly>();

    internal ICollection<Type> TypesToScan { get; set; } = new List<Type>();

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

    public PowerPipeVisualizationConfiguration ScanFromType(Type type)
    {
        TypesToScan.Add(type);

        return this;
    }

    public PowerPipeVisualizationConfiguration ScanFromTypes(params Type[] types)
    {
        foreach (var type in types)
        {
            ScanFromType(type);
        }

        return this;
    }
}
