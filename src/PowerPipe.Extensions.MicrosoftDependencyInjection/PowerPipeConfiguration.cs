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

    internal ICollection<Assembly> AssembliesToRegister { get; set; } = new List<Assembly>();

    internal ICollection<ServiceDescriptor> StepsToOverrideLifetime { get; set; } = new List<ServiceDescriptor>();

    internal ServiceLifetime FactoryDefaultLifetime { get; set; } = ServiceLifetime.Transient;

    internal ServiceLifetime StepsDefaultLifetime { get; set; } = ServiceLifetime.Transient;

    /// <summary>
    /// Register assembly to search implementations of steps from
    /// </summary>
    /// <param name="assembly">assembly where search will be</param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration RegisterServicesFromAssembly(Assembly assembly)
    {
        AssembliesToRegister.Add(assembly);

        return this;
    }

    /// <summary>
    /// Register array of assemblies to search implementations of steps from
    /// </summary>
    /// <param name="assemblies">array of assemblies where search will be</param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration RegisterServicesFromAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            RegisterServicesFromAssembly(assembly);
        }

        return this;
    }

    /// <summary>
    /// Sets TypeEvaluator
    /// </summary>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration ApplyTypeEvaluation(Func<Type, bool> typeEvaluator)
    {
        TypeEvaluator = typeEvaluator;

        return this;
    }

    /// <summary>
    /// Changes default (Transient) factory registration life time
    /// </summary>
    /// <param name="lifetime"></param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration ChangeFactoryDefaultLifetime(ServiceLifetime lifetime)
    {
        if (FactoryDefaultLifetime == lifetime)
        {
            return this;
        }

        FactoryDefaultLifetime = lifetime;

        return this;
    }

    /// <summary>
    /// Changes default (Transient) steps registration life time
    /// </summary>
    /// <param name="lifetime"></param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration ChangeStepsDefaultLifetime(ServiceLifetime lifetime)
    {
        if (StepsDefaultLifetime == lifetime)
        {
            return this;
        }

        StepsDefaultLifetime = lifetime;

        return this;
    }

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <typeparam name="TServiceType">Type of step implementation</typeparam>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddTransient<TServiceType>() => AddTransient(typeof(TServiceType));

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <param name="type">Type of step implementation</param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddTransient(Type type) => Add(type, ServiceLifetime.Transient);

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <typeparam name="TServiceType">Type of step implementation</typeparam>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddScoped<TServiceType>() => AddScoped(typeof(TServiceType));

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <param name="type">Type of step implementation</param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddScoped(Type type) => Add(type, ServiceLifetime.Scoped);

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <typeparam name="TServiceType">Type of step implementation</typeparam>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddSingleton<TServiceType>() => AddSingleton(typeof(TServiceType));

    /// <summary>
    /// Register implementation with specific lifetime
    /// </summary>
    /// <param name="type">Type of step implementation</param>
    /// <returns>Instance of PowerPipeConfiguration</returns>
    public PowerPipeConfiguration AddSingleton(Type type) => Add(type, ServiceLifetime.Singleton);

    private PowerPipeConfiguration Add(Type serviceType, ServiceLifetime serviceLifetime)
    {
        StepsToOverrideLifetime.Add(new ServiceDescriptor(serviceType, serviceType, serviceLifetime));

        return this;
    }
}
