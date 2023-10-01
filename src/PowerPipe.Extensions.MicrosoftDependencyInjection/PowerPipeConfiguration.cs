using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PowerPipe.Interfaces;

namespace PowerPipe.Extensions.MicrosoftDependencyInjection;

public class PowerPipeConfiguration
{
    /// <summary>
    /// Optional filter for types to register. Default value is a function returning true.
    /// </summary>
    public Func<Type, bool> TypeEvaluator { get; set; } = t => true;
    
    /// <summary>
    /// Type of notification publisher strategy to register. If set, overrides <see cref="NotificationPublisher"/>
    /// </summary>
    public Type? NotificationPublisherType { get; set; }

    /// <summary>
    /// Service lifetime to register services under. Default value is <see cref="ServiceLifetime.Transient"/>
    /// </summary>
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;

    internal List<Assembly> AssembliesToRegister { get; } = new();

    /// <summary>
    /// List of behaviors to register in specific order
    /// </summary>
    public List<ServiceDescriptor> BehaviorsToRegister { get; } = new();

    /// <summary>
    /// List of stream behaviors to register in specific order
    /// </summary>
    public List<ServiceDescriptor> StreamBehaviorsToRegister { get; } = new();

    /// <summary>
    /// List of request pre processors to register in specific order
    /// </summary>
    public List<ServiceDescriptor> RequestPreProcessorsToRegister { get; } = new();

    /// <summary>
    /// List of request post processors to register in specific order
    /// </summary>
    public List<ServiceDescriptor> RequestPostProcessorsToRegister { get; } = new();

    /// <summary>
    /// Register various handlers from assembly containing given type
    /// </summary>
    /// <typeparam name="T">Type from assembly to scan</typeparam>
    /// <returns>This</returns>
    public PowerPipeConfiguration RegisterServicesFromAssemblyContaining<T>()
        => RegisterServicesFromAssemblyContaining(typeof(T));

    /// <summary>
    /// Register various handlers from assembly containing given type
    /// </summary>
    /// <param name="type">Type from assembly to scan</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration RegisterServicesFromAssemblyContaining(Type type)
        => RegisterServicesFromAssembly(type.Assembly);

    /// <summary>
    /// Register various handlers from assembly
    /// </summary>
    /// <param name="assembly">Assembly to scan</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration RegisterServicesFromAssembly(Assembly assembly)
    {
        AssembliesToRegister.Add(assembly);

        return this;
    }

    /// <summary>
    /// Register various handlers from assemblies
    /// </summary>
    /// <param name="assemblies">Assemblies to scan</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration RegisterServicesFromAssemblies(
        params Assembly[] assemblies)
    {
        AssembliesToRegister.AddRange(assemblies);

        return this;
    }

    /// <summary>
    /// Register a closed behavior type
    /// </summary>
    /// <typeparam name="TServiceType">Closed behavior interface type</typeparam>
    /// <typeparam name="TImplementationType">Closed behavior implementation type</typeparam>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddBehavior<TServiceType, TImplementationType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddBehavior(typeof(TServiceType), typeof(TImplementationType), serviceLifetime);

    /// <summary>
    /// Register a closed behavior type against all <see cref="IPipelineBehavior{TRequest,TResponse}"/> implementations
    /// </summary>
    /// <typeparam name="TImplementationType">Closed behavior implementation type</typeparam>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddBehavior<TImplementationType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        return AddBehavior(typeof(TImplementationType), serviceLifetime);
    }

    /// <summary>
    /// Register a closed behavior type against all <see cref="IPipelineBehavior{TRequest,TResponse}"/> implementations
    /// </summary>
    /// <param name="implementationType">Closed behavior implementation type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddBehavior(Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        var implementedGenericInterfaces = implementationType.FindInterfacesThatClose(typeof(IPipelineStep<>)).ToList();

        if (implementedGenericInterfaces.Count == 0)
        {
            throw new InvalidOperationException($"{implementationType.Name} must implement {typeof(IPipelineStep<>).FullName}");
        }

        foreach (var implementedBehaviorType in implementedGenericInterfaces)
        {
            BehaviorsToRegister.Add(new ServiceDescriptor(implementedBehaviorType, implementationType, serviceLifetime));
        }

        return this;
    }

    /// <summary>
    /// Register a closed behavior type
    /// </summary>
    /// <param name="serviceType">Closed behavior interface type</param>
    /// <param name="implementationType">Closed behavior implementation type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddBehavior(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        BehaviorsToRegister.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));

        return this;
    }

    /// <summary>
    /// Registers an open behavior type against the <see cref="IPipelineBehavior{TRequest,TResponse}"/> open generic interface type
    /// </summary>
    /// <param name="openBehaviorType">An open generic behavior type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddOpenBehavior(Type openBehaviorType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        if (!openBehaviorType.IsGenericType)
        {
            throw new InvalidOperationException($"{openBehaviorType.Name} must be generic");
        }

        var implementedGenericInterfaces = openBehaviorType.GetInterfaces().Where(i => i.IsGenericType).Select(i => i.GetGenericTypeDefinition());
        var implementedOpenBehaviorInterfaces = new HashSet<Type>(implementedGenericInterfaces.Where(i => i == typeof(IPipelineStep<>)));

        if (implementedOpenBehaviorInterfaces.Count == 0)
        {
            throw new InvalidOperationException($"{openBehaviorType.Name} must implement {typeof(IPipelineStep<>).FullName}");
        }

        foreach (var openBehaviorInterface in implementedOpenBehaviorInterfaces)
        {
            BehaviorsToRegister.Add(new ServiceDescriptor(openBehaviorInterface, openBehaviorType, serviceLifetime));
        }

        return this;
    }

    /// <summary>
    /// Register a closed stream behavior type
    /// </summary>
    /// <typeparam name="TServiceType">Closed stream behavior interface type</typeparam>
    /// <typeparam name="TImplementationType">Closed stream behavior implementation type</typeparam>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddStreamBehavior<TServiceType, TImplementationType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddStreamBehavior(typeof(TServiceType), typeof(TImplementationType), serviceLifetime);
    
    /// <summary>
    /// Register a closed stream behavior type
    /// </summary>
    /// <param name="serviceType">Closed stream behavior interface type</param>
    /// <param name="implementationType">Closed stream behavior implementation type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddStreamBehavior(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        StreamBehaviorsToRegister.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));

        return this;
    }
    
    /// <summary>
    /// Register a closed stream behavior type against all <see cref="IStreamPipelineBehavior{TRequest,TResponse}"/> implementations
    /// </summary>
    /// <typeparam name="TImplementationType">Closed stream behavior implementation type</typeparam>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddStreamBehavior<TImplementationType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddStreamBehavior(typeof(TImplementationType), serviceLifetime);
    
    /// <summary>
    /// Register a closed stream behavior type against all <see cref="IStreamPipelineBehavior{TRequest,TResponse}"/> implementations
    /// </summary>
    /// <param name="implementationType">Closed stream behavior implementation type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddStreamBehavior(Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        var implementedGenericInterfaces = implementationType.FindInterfacesThatClose(typeof(IPipelineStep<>)).ToList();

        if (implementedGenericInterfaces.Count == 0)
        {
            throw new InvalidOperationException($"{implementationType.Name} must implement {typeof(IPipelineStep<>).FullName}");
        }

        foreach (var implementedBehaviorType in implementedGenericInterfaces)
        {
            StreamBehaviorsToRegister.Add(new ServiceDescriptor(implementedBehaviorType, implementationType, serviceLifetime));
        }

        return this;
    }
    
    /// <summary>
    /// Registers an open stream behavior type against the <see cref="IStreamPipelineBehavior{TRequest,TResponse}"/> open generic interface type
    /// </summary>
    /// <param name="openBehaviorType">An open generic stream behavior type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddOpenStreamBehavior(Type openBehaviorType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        if (!openBehaviorType.IsGenericType)
        {
            throw new InvalidOperationException($"{openBehaviorType.Name} must be generic");
        }

        var implementedGenericInterfaces = openBehaviorType.GetInterfaces().Where(i => i.IsGenericType).Select(i => i.GetGenericTypeDefinition());
        var implementedOpenBehaviorInterfaces = new HashSet<Type>(implementedGenericInterfaces.Where(i => i == typeof(IPipelineStep<>)));

        if (implementedOpenBehaviorInterfaces.Count == 0)
        {
            throw new InvalidOperationException($"{openBehaviorType.Name} must implement {typeof(IPipelineStep<>).FullName}");
        }

        foreach (var openBehaviorInterface in implementedOpenBehaviorInterfaces)
        {
            StreamBehaviorsToRegister.Add(new ServiceDescriptor(openBehaviorInterface, openBehaviorType, serviceLifetime));
        }

        return this;
    }

    /// <summary>
    /// Register a closed request pre processor type
    /// </summary>
    /// <typeparam name="TServiceType">Closed request pre processor interface type</typeparam>
    /// <typeparam name="TImplementationType">Closed request pre processor implementation type</typeparam>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddRequestPreProcessor<TServiceType, TImplementationType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddRequestPreProcessor(typeof(TServiceType), typeof(TImplementationType), serviceLifetime);
    
    /// <summary>
    /// Register a closed request pre processor type
    /// </summary>
    /// <param name="serviceType">Closed request pre processor interface type</param>
    /// <param name="implementationType">Closed request pre processor implementation type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddRequestPreProcessor(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        RequestPreProcessorsToRegister.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));

        return this;
    }

    /// <summary>
    /// Register a closed request pre processor type against all <see cref="IRequestPreProcessor{TRequest}"/> implementations
    /// </summary>
    /// <typeparam name="TImplementationType">Closed request pre processor implementation type</typeparam>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddRequestPreProcessor<TImplementationType>(
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddRequestPreProcessor(typeof(TImplementationType), serviceLifetime);

    /// <summary>
    /// Register a closed request pre processor type against all <see cref="IRequestPreProcessor{TRequest}"/> implementations
    /// </summary>
    /// <param name="implementationType">Closed request pre processor implementation type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddRequestPreProcessor(Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        var implementedGenericInterfaces = implementationType.FindInterfacesThatClose(typeof(IPipelineStep<>)).ToList();

        if (implementedGenericInterfaces.Count == 0)
        {
            throw new InvalidOperationException($"{implementationType.Name} must implement {typeof(IPipelineStep<>).FullName}");
        }

        foreach (var implementedPreProcessorType in implementedGenericInterfaces)
        {
            RequestPreProcessorsToRegister.Add(new ServiceDescriptor(implementedPreProcessorType, implementationType, serviceLifetime));
        }
        
        return this;
    }
    
    /// <summary>
    /// Registers an open request pre processor type against the <see cref="IRequestPreProcessor{TRequest}"/> open generic interface type
    /// </summary>
    /// <param name="openBehaviorType">An open generic request pre processor type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddOpenRequestPreProcessor(Type openBehaviorType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        if (!openBehaviorType.IsGenericType)
        {
            throw new InvalidOperationException($"{openBehaviorType.Name} must be generic");
        }

        var implementedGenericInterfaces = openBehaviorType.GetInterfaces().Where(i => i.IsGenericType).Select(i => i.GetGenericTypeDefinition());
        var implementedOpenBehaviorInterfaces = new HashSet<Type>(implementedGenericInterfaces.Where(i => i == typeof(IPipelineStep<>)));

        if (implementedOpenBehaviorInterfaces.Count == 0)
        {
            throw new InvalidOperationException($"{openBehaviorType.Name} must implement {typeof(IPipelineStep<>).FullName}");
        }

        foreach (var openBehaviorInterface in implementedOpenBehaviorInterfaces)
        {
            RequestPreProcessorsToRegister.Add(new ServiceDescriptor(openBehaviorInterface, openBehaviorType, serviceLifetime));
        }

        return this;
    }
    
    /// <summary>
    /// Register a closed request post processor type
    /// </summary>
    /// <typeparam name="TServiceType">Closed request post processor interface type</typeparam>
    /// <typeparam name="TImplementationType">Closed request post processor implementation type</typeparam>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddRequestPostProcessor<TServiceType, TImplementationType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddRequestPostProcessor(typeof(TServiceType), typeof(TImplementationType), serviceLifetime);
    
    /// <summary>
    /// Register a closed request post processor type
    /// </summary>
    /// <param name="serviceType">Closed request post processor interface type</param>
    /// <param name="implementationType">Closed request post processor implementation type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddRequestPostProcessor(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        RequestPostProcessorsToRegister.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));

        return this;
    }
 
    /// <summary>
    /// Register a closed request post processor type against all <see cref="IRequestPostProcessor{TRequest,TResponse}"/> implementations
    /// </summary>
    /// <typeparam name="TImplementationType">Closed request post processor implementation type</typeparam>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddRequestPostProcessor<TImplementationType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        => AddRequestPostProcessor(typeof(TImplementationType), serviceLifetime);
    
    /// <summary>
    /// Register a closed request post processor type against all <see cref="IRequestPostProcessor{TRequest,TResponse}"/> implementations
    /// </summary>
    /// <param name="implementationType">Closed request post processor implementation type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddRequestPostProcessor(Type implementationType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        var implementedGenericInterfaces = implementationType.FindInterfacesThatClose(typeof(IPipelineStep<>)).ToList();

        if (implementedGenericInterfaces.Count == 0)
        {
            throw new InvalidOperationException($"{implementationType.Name} must implement {typeof(IPipelineStep<>).FullName}");
        }

        foreach (var implementedPostProcessorType in implementedGenericInterfaces)
        {
            RequestPostProcessorsToRegister.Add(new ServiceDescriptor(implementedPostProcessorType, implementationType, serviceLifetime));
        }
        return this;
    }
    
    /// <summary>
    /// Registers an open request post processor type against the <see cref="IRequestPostProcessor{TRequest,TResponse}"/> open generic interface type
    /// </summary>
    /// <param name="openBehaviorType">An open generic request post processor type</param>
    /// <param name="serviceLifetime">Optional service lifetime, defaults to <see cref="ServiceLifetime.Transient"/>.</param>
    /// <returns>This</returns>
    public PowerPipeConfiguration AddOpenRequestPostProcessor(Type openBehaviorType, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        if (!openBehaviorType.IsGenericType)
        {
            throw new InvalidOperationException($"{openBehaviorType.Name} must be generic");
        }

        var implementedGenericInterfaces = openBehaviorType.GetInterfaces().Where(i => i.IsGenericType).Select(i => i.GetGenericTypeDefinition());
        var implementedOpenBehaviorInterfaces = new HashSet<Type>(implementedGenericInterfaces.Where(i => i == typeof(IPipelineStep<>)));

        if (implementedOpenBehaviorInterfaces.Count == 0)
        {
            throw new InvalidOperationException($"{openBehaviorType.Name} must implement {typeof(IPipelineStep<>).FullName}");
        }

        foreach (var openBehaviorInterface in implementedOpenBehaviorInterfaces)
        {
            RequestPostProcessorsToRegister.Add(new ServiceDescriptor(openBehaviorInterface, openBehaviorType, serviceLifetime));
        }

        return this;
    }
}