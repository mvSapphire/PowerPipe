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
    /// <returns></returns>
    public PowerPipeConfiguration ChangeDefaultLifetime(ServiceLifetime lifetime)
    {
        if (DefaultLifetime == lifetime)
            return this;

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
    /// Register implementation with specific lifetime. By default - Transient.
    /// </summary>
    /// <param name="serviceLifetime">ServiceLifetime.Transient by default</param>
    /// <typeparam name="TServiceType">Type of step implementation</typeparam>
    /// <returns></returns>
    public PowerPipeConfiguration AddBehavior<TServiceType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddBehavior(typeof(TServiceType), serviceLifetime);

    /// <summary>
    /// Register implementation with specific lifetime. By default - Transient.
    /// </summary>
    /// <param name="type">Type of step implementation</param>
    /// <param name="serviceLifetime">ServiceLifetime.Transient by default</param>
    /// <returns></returns>
    public PowerPipeConfiguration AddBehavior(Type type, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddBehavior(type, type, serviceLifetime);

    #region PrivateMethods

    private PowerPipeConfiguration AddBehavior(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime)
    {
        BehaviorsToRegister.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));

        return this;
    }

    #endregion
}
