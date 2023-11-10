using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PowerPipe.Factories;
using PowerPipe.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

internal static class ServiceRegistrar
{
    public static void AddPowerPipeClasses(IServiceCollection services, PowerPipeConfiguration configuration)
    {
        var assembliesToScan = configuration.AssembliesToRegister.Distinct().ToArray();
        ConnectImplementationsToTypes(typeof(IPipelineParallelStep<>), services, assembliesToScan, configuration);
        ConnectImplementationsToTypes(typeof(IPipelineCompensationStep<>), services, assembliesToScan, configuration);
        ConnectImplementationsToTypes(typeof(IPipelineStep<>), services, assembliesToScan, configuration);
    }
    
    public static void AddRequiredServices(IServiceCollection services, PowerPipeConfiguration serviceConfiguration)
    {
        services.TryAdd(new ServiceDescriptor(typeof(IPipelineStepFactory), typeof(PipelineStepFactory), serviceConfiguration.FactoryDefaultLifetime));

        foreach (var serviceDescriptor in serviceConfiguration.StepsToOverrideLifetime)
        {
            // this is for future, when we need search by interface and not an concrete implementation
            if (serviceDescriptor.ServiceType != serviceDescriptor.ImplementationType)
            {
                services.TryAddEnumerable(serviceDescriptor);
                continue;
            }

            services.TryAdd(serviceDescriptor);
        }
    }

    private static void ConnectImplementationsToTypes(
        Type openRequestInterface, IServiceCollection services, IEnumerable<Assembly> assembliesToScan, PowerPipeConfiguration configuration)
    {
        var concretions = new List<Type>();
        var interfaces = new List<Type>();
        foreach (var type in assembliesToScan.SelectMany(a => a.DefinedTypes).Where(configuration.TypeEvaluator))
        {
            var interfaceTypes = type.FindInterfaces(openRequestInterface).Distinct().ToArray();
            if (!interfaceTypes.Any())
            {
                continue;
            }

            if (type.IsConcrete())
            {
                concretions.Add(type);
            }

            foreach (var interfaceType in interfaceTypes)
            {
                interfaces.Fill(interfaceType);
            }
        }

        foreach (var inf in interfaces)
        {
            var exactMatches = concretions.Where(x => x.CanBeCastTo(inf)).ToList();
        
            if (exactMatches.Count > 1)
            {
                exactMatches.RemoveAll(m => !IsMatchingWithInterface(m, inf));
            }

            foreach (var type in exactMatches.Where(type => configuration.StepsToOverrideLifetime.All(c => c.ImplementationType != type)))
            {
                services.TryAdd(new ServiceDescriptor(type, type, configuration.StepsDefaultLifetime));
            }

            if (!inf.IsOpenGeneric())
            {
                AddConcretionsThatCouldBeClosed(inf, concretions, services);
            }
        }
    }

    private static bool IsMatchingWithInterface(Type handlerType, Type handlerInterface)
    {
        if (handlerType == null || handlerInterface == null)
        {
            return false;
        }

        if (handlerType.IsInterface && handlerType.GenericTypeArguments.SequenceEqual(handlerInterface.GenericTypeArguments))
        {
            return true;
        }

        return IsMatchingWithInterface(handlerType.GetInterface(handlerInterface.Name), handlerInterface);
    }

    private static void AddConcretionsThatCouldBeClosed(
        Type inInterface, IEnumerable<Type> concretions, IServiceCollection services)
    {
        foreach (var type in concretions.Where(x => x.IsOpenGeneric() && x.CouldCloseTo(inInterface)))
        {
            try
            {
                services.TryAddTransient(inInterface, type.MakeGenericType(inInterface.GenericTypeArguments));
            }
            catch
            {
                // ignored
            }
        }
    }

    private static bool CouldCloseTo(this Type openConcretion, Type closedInterface)
    {
        var openInterface = closedInterface.GetGenericTypeDefinition();
        var arguments = closedInterface.GenericTypeArguments;

        var concreteArguments = openConcretion.GenericTypeArguments;
        return arguments.Length == concreteArguments.Length && openConcretion.CanBeCastTo(openInterface);
    }

    private static bool CanBeCastTo(this Type pluggedType, Type pluginType)
    {
        if (pluggedType is null)
        {
            return false;
        }

        return pluggedType == pluginType || pluginType.IsAssignableFrom(pluggedType);
    }

    private static bool IsOpenGeneric(this Type type)
    {
        return type.IsGenericTypeDefinition || type.ContainsGenericParameters;
    }

    private static IEnumerable<Type> FindInterfaces(this Type pluggedType, Type templateType)
    {
        if (pluggedType is null || !pluggedType.IsConcrete() || !templateType.IsInterface)
        {
            yield break;
        }

        foreach (var interfaceType in pluggedType.GetInterfaces()
                     .Where(type => type.IsGenericType && type.GetGenericTypeDefinition() == templateType))
        {
            yield return interfaceType;
        }
    }

    private static bool IsConcrete(this Type type)
    {
        return !type.IsAbstract && !type.IsInterface;
    }

    private static void Fill<T>(this ICollection<T> list, T value)
    {
        if (list.Contains(value))
        {
            return;
        }
        
        list.Add(value);
    }
}
