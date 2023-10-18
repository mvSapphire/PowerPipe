using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PowerPipe.Extensions.MicrosoftDependencyInjection;

public class PowerPipeConfiguration
{
    public Func<Type, bool> TypeEvaluator { get; set; } = t => true;

    internal ICollection<Assembly> AssembliesToRegister { get; } = new List<Assembly>();

    internal ICollection<ServiceDescriptor> BehaviorsToRegister { get; } = new List<ServiceDescriptor>();
    
    public PowerPipeConfiguration RegisterServicesFromAssembly(Assembly assembly)
    {
        AssembliesToRegister.Add(assembly);

        return this;
    }

    public PowerPipeConfiguration RegisterServicesFromAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            RegisterServicesFromAssembly(assembly);
        }

        return this;
    }

    public PowerPipeConfiguration AddBehavior<TServiceType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddBehavior(typeof(TServiceType), typeof(TServiceType), serviceLifetime);

    private PowerPipeConfiguration AddBehavior(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        BehaviorsToRegister.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));

        return this;
    }
}