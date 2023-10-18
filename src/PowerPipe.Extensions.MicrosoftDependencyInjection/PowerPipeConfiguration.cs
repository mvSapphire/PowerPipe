using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PowerPipe.Extensions.MicrosoftDependencyInjection;

/// <summary>
/// Configuration class for automatic steps registration
/// </summary>
public class PowerPipeConfiguration
{
    /// <summary>
    /// Used to evaluate a type from assembly. Is it suitable for registration 
    /// </summary>
    public Func<Type, bool> TypeEvaluator { get; set; } = _ => true;

    internal ICollection<Assembly> AssembliesToRegister { get; } = new List<Assembly>();

    internal ICollection<ServiceDescriptor> BehaviorsToRegister { get; } = new List<ServiceDescriptor>();
    
    /// <summary>
    /// Register assembly to search implementations of steps from
    /// </summary>
    /// <param name="assembly">assembly where search will be</param>
    /// <returns>instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration RegisterServicesFromAssembly(Assembly assembly)
    {
        AssembliesToRegister.Add(assembly);

        return this;
    }

    /// <summary>
    /// Register array of assemblies to search implementations of steps from
    /// </summary>
    /// <param name="assemblies">array of assemblies where search will be</param>
    /// <returns>instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration RegisterServicesFromAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            RegisterServicesFromAssembly(assembly);
        }

        return this;
    }

    /// <summary>
    /// Register implementation with specific lifetime. By default - Transient.
    /// </summary>
    /// <param name="serviceLifetime">ServiceLifetime.Transient by default</param>
    /// <typeparam name="TServiceType">Type of step implementation</typeparam>
    /// <returns></returns>
    public PowerPipeConfiguration AddBehavior<TServiceType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddBehavior(typeof(TServiceType), typeof(TServiceType), serviceLifetime);

    private PowerPipeConfiguration AddBehavior(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        BehaviorsToRegister.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));

        return this;
    }
}
