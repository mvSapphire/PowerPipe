using System;
using System.Collections.Generic;
using System.Reflection;

namespace PowerPipe.Visualization.Configurations;

/// <summary>
/// Represents the configuration settings for PowerPipe visualization.
/// </summary>
public class PowerPipeVisualizationConfiguration
{
    internal ICollection<Assembly> AssembliesToScan { get; set; } = new List<Assembly>();

    internal ICollection<Type> TypesToScan { get; set; } = new List<Type>();

    /// <summary>
    /// Adds an assembly to the list of assemblies to be scanned for types.
    /// </summary>
    /// <param name="assembly">The assembly to be scanned.</param>
    /// <returns>The updated PowerPipeVisualizationConfiguration instance.</returns>
    public PowerPipeVisualizationConfiguration ScanFromAssembly(Assembly assembly)
    {
        AssembliesToScan.Add(assembly);

        return this;
    }

    /// <summary>
    /// Adds multiple assemblies to the list of assemblies to be scanned for types.
    /// </summary>
    /// <param name="assemblies">The assemblies to be scanned.</param>
    /// <returns>The updated PowerPipeVisualizationConfiguration instance.</returns>
    public PowerPipeVisualizationConfiguration ScanFromAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            ScanFromAssembly(assembly);
        }

        return this;
    }

    /// <summary>
    /// Adds a type to the list of types to be scanned.
    /// </summary>
    /// <param name="type">The type to be scanned.</param>
    /// <returns>The updated PowerPipeVisualizationConfiguration instance.</returns>
    public PowerPipeVisualizationConfiguration ScanFromType(Type type)
    {
        TypesToScan.Add(type);

        return this;
    }

    /// <summary>
    /// Adds multiple types to the list of types to be scanned.
    /// </summary>
    /// <param name="types">The types to be scanned.</param>
    /// <returns>The updated PowerPipeVisualizationConfiguration instance.</returns>
    public PowerPipeVisualizationConfiguration ScanFromTypes(params Type[] types)
    {
        foreach (var type in types)
        {
            ScanFromType(type);
        }

        return this;
    }
}
