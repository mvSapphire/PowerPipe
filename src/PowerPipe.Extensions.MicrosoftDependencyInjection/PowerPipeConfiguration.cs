using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

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

    internal ServiceLifetime DefaultLifetime { get; private set; } = ServiceLifetime.Transient;

    /// <summary>
    /// Changes default (Transient) service registration life time
    /// </summary>
    /// <param name="lifetime"></param>
    /// <returns>instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration ChangeDefaultLifetime(ServiceLifetime lifetime)
    {
        if (DefaultLifetime == lifetime)
        {
            return this;
        }

        DefaultLifetime = lifetime;

        return this;
    }

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
    /// Register implementation with specific lifetime
    /// </summary>
    /// <typeparam name="TServiceType">Type of step implementation</typeparam>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddTransient<TServiceType>() =>
        AddTransient(typeof(TServiceType));

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <param name="type">Type of step implementation</param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddTransient(Type type) =>
        Add(type, ServiceLifetime.Transient);

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <typeparam name="TServiceType">Type of step implementation</typeparam>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddScoped<TServiceType>() =>
        AddScoped(typeof(TServiceType));

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <param name="type">Type of step implementation</param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddScoped(Type type) =>
        Add(type, ServiceLifetime.Scoped);

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <typeparam name="TServiceType">Type of step implementation</typeparam>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddSingleton<TServiceType>() =>
        AddSingleton(typeof(TServiceType));

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <param name="type">Type of step implementation</param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddSingleton(Type type) =>
        Add(type, ServiceLifetime.Singleton);

    private PowerPipeConfiguration Add(Type serviceType, ServiceLifetime serviceLifetime)
    {
        BehaviorsToRegister.Add(new ServiceDescriptor(serviceType, serviceType, serviceLifetime));

        return this;
    }
}
