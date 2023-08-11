# PowerPipe: A .NET Library for Constructing Advanced Pipelines with Fluent Interface

[![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/mvSapphire/PowerPipe/dotnet.yml)](https://github.com/mvSapphire/PowerPipe/actions)
[![Nuget](https://img.shields.io/nuget/v/PowerPipe)](https://www.nuget.org/packages/PowerPipe)
[![Nuget](https://img.shields.io/nuget/dt/PowerPipe)]([https://www.nuget.org/packages/PowerPipe](https://www.nuget.org/stats/packages/PowerPipe?groupby=Version))

PowerPipe is a versatile .NET library designed to streamline the process of building advanced pipelines using a fluent interface. The primary objective of this project is to eliminate the need for writing boilerplate code when implementing pipelines.

**Table of Contents**
- [Features and Benefits](#features-and-benefits)
- [Installation](#installation)
  - [Package Manager Console](#package-manager-console)
  - [.NET CLI](#net-cli)
- [Core Components](#core-components)
  - [PipelineContext: Abstract Context Class](#pipelinecontext-abstract-context-class)
  - [Pipeline: Constructing Pipelines](#pipeline-constructing-pipelines)
    - [Pipeline Methods](#pipeline-methods)
- [Pipeline Steps: Building Blocks of Pipelines](#pipeline-steps-building-blocks-of-pipelines)
  - [IPipelineStep Interface](#ipipelinestep-interface)
    - [ExecuteAsync Method](#executeasync-method)
  - [Pre-implemented Steps](#pre-implemented-steps)
- [PipelineStepFactory: Step Factory for Dependency Injection](#pipelinestepfactory-step-factory-for-dependency-injection)
- [PipelineBuilder: Building Pipelines](#pipelinebuilder-building-pipelines)
  - [PipelineBuilder Methods](#pipelinebuilder-methods)
- [Extensions: Microsoft Dependency Injection](#extensions-microsoft-dependency-injection)
  - [Methods](#methods)
- [Examples](#examples)
- [Contributors are Welcome!](#contributors-are-welcome)

## Features and Benefits

- Developed using .NET 6 for optimal performance and compatibility.
- Offers a readable and intuitive API that simplifies pipeline construction.

## Installation

You can easily integrate PowerPipe into your project by installing the required NuGet packages. Use the following commands in the Package Manager Console or .NET CLI:

### Package Manager Console

```powershell
Install-Package PowerPipe
Install-Package PowerPipe.Extensions.MicrosoftDependencyInjection
```

### .NET CLI

```bash
dotnet add package PowerPipe
dotnet add package PowerPipe.Extensions.MicrosoftDependencyInjection
```

## Core Components

### PipelineContext: Abstract Context Class

`PipelineContext` serves as the foundational abstract context class from which all specific contexts should inherit. It provides a generic representation that encapsulates the outcome of the pipeline.

```csharp
public abstract class PipelineContext<TResult>
    where TResult : class
{
    public abstract TResult GetPipelineResult();
}
```

### Pipeline: Constructing Pipelines

The `Pipeline` class represents a generic pipeline implementation. It is responsible for connecting all pipeline steps internally and executing the initial step to kickstart the process.

```csharp
public class Pipeline<TContext, TResult> : IPipeline<TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    // Implementation details...
}
```

#### Pipeline Methods

##### RunAsync

```csharp
public async Task<TResult> RunAsync(CancellationToken cancellationToken, bool returnResult = true)
```

The `RunAsync` method is the primary entry point for executing the pipeline. It allows you to specify whether you need the result of the pipeline or not. For nested pipelines, the `returnResult` parameter should be set to `false`.

### Pipeline Steps: Building Blocks of Pipelines

#### IPipelineStep Interface

The `IPipelineStep` interface defines the blueprint for creating custom pipeline steps. You should implement this interface to describe your own pipeline steps.

```csharp
public interface IPipelineStep<TContext>
{
    IPipelineStep<TContext> NextStep { get; set; }
    Task ExecuteAsync(TContext context, CancellationToken cancellationToken);
}
```

##### ExecuteAsync Method

The `ExecuteAsync` method is the core logic execution point for a pipeline step.

#### Pre-implemented Steps

Several steps are implemented and executed internally, contributing to the pipeline flow. While they are abstracted away, it's beneficial to be aware of them.

- **LazyStep:** A step that acts as a 'decorator' for other steps, ensuring thread safety.

- **AddIfStep:** Adds a step to the main pipeline based on a specified predicate.

- **AddIfElseStep:** Adds a step to the pipeline conditionally, branching based on a predicate.

- **IfPipelineStep:** Adds a nested pipeline based on a predicate.

- **FinishStep:** Automatically added as the final step of the pipeline.

### PipelineStepFactory: Step Factory for Dependency Injection

The `PipelineStepFactory` class implements the `IPipelineStepFactory` interface, responsible for obtaining steps from Dependency Injection (DI).

```csharp
public class PipelineStepFactory : IPipelineStepFactory
{
    // Implementation details...
}
```

### PipelineBuilder: Building Pipelines

The `PipelineBuilder` class is the primary tool for constructing pipelines. It allows you to define the sequence of steps and their conditions.

```csharp
public sealed class PipelineBuilder<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    // Implementation details...
}
```

#### PipelineBuilder Methods

##### Add

```csharp
public PipelineBuilder<TContext, TResult> Add<T>()
    where T : IPipelineStep<TContext>
```

The `Add` method appends a step to the end of the pipeline.

##### AddIf

```csharp
public PipelineBuilder<TContext, TResult> AddIf<T>(Predicate<TContext> predicate)
    where T : IPipelineStep<TContext>
```

The `AddIf` method adds a step to the pipeline based on a specified predicate.

##### If

```csharp
public PipelineBuilder<TContext, TResult> If(Func<bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
```

The `If` method adds a nested pipeline based on a predicate.

##### If (with context)

```csharp
public PipelineBuilder<TContext, TResult> If(Func<TContext, bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
```

Similar to the previous method, the `If` method adds a nested pipeline, but this time it accepts a predicate with access to the `TContext` object.

##### Build

```csharp
public IPipeline<TResult> Build()
```

The `Build` method finalizes the pipeline construction by adding a `FinishStep<TContext>` and returning the built pipeline as an `IPipeline<TResult>` object.

### Extensions: Microsoft Dependency Injection

The `PowerPipe.Extensions.MicrosoftDependencyInjection` extension provides integration with Microsoft Dependency Injection.

#### Methods

##### AddPowerPipe

```csharp
public static IServiceCollection AddPowerPipe(this IServiceCollection serviceCollection)
```

The `AddPowerPipe` method adds the `IPipelineStepFactory` to the Dependency Injection container.

##### AddPowerPipeStep

```csharp
public static IServiceCollection AddPowerPipeStep<TStep, TContext>(this IServiceCollection serviceCollection, ServiceLifetime lifetime = ServiceLifetime.Transient)
    where TStep : class, IPipelineStep<TContext>
    where TContext : PipelineContext<Type>
```

The `AddPowerPipeStep` method adds your custom pipeline steps to the Dependency Injection container with a transient scope by default.

## Examples

**_Example: Processing Customer Orders_**

Suppose you're working on an e-commerce platform, and you need to process incoming customer orders. Each order involves several steps, such as validation, pricing calculation, and inventory management. Let's see how PowerPipe can help you build a pipeline to handle this process.

_Step 1: Define the Pipeline Context_

First, let's create a pipeline context that will hold the information related to the order processing. We'll define a simple context class that contains order details:

```csharp
public class OrderContext : PipelineContext<OrderResult>
{
    // Properties and methods specific to the context
}
```

_Step 2: Implement Pipeline Steps_

Next, we'll create individual steps for our pipeline. For simplicity, let's focus on two steps: validation and pricing calculation. We'll implement these steps by implementing the IPipelineStep<OrderContext> interface:

```csharp
public class ValidationStep : IPipelineStep<OrderContext>
{
    public IPipelineStep<OrderContext> NextStep { get; set; }

    public async Task ExecuteAsync(OrderContext context, CancellationToken cancellationToken)
    {
        // Validate the order and update the context
        // Implement your validation logic here
    }
}

public class PricingStep : IPipelineStep<OrderContext>
{
    public IPipelineStep<OrderContext> NextStep { get; set; }

    public async Task ExecuteAsync(OrderContext context, CancellationToken cancellationToken)
    {
        // Calculate pricing for the order and update the context
        // Implement your pricing logic here
    }
}
```

_Step 3: Build the Pipeline_

Now, let's use PowerPipe to build our pipeline. We'll create a pipeline builder and add our steps to it based on certain conditions:

```csharp
var pipeline = new PipelineBuilder<OrderContext, OrderResult>()
    .Add<ValidationStep>()
    .AddIf<PricingStep>(context => context.IsValid)
    .Build();
```
In this example, the `AddIf` method adds the PricingStep only if the order is valid.

_Step 4: Execute the Pipeline_

Finally, we'll execute the pipeline using the `RunAsync` method:

```csharp
var orderContext = new OrderContext();
var orderResult = await pipeline.RunAsync(orderContext, CancellationToken cancellationToken);
```

_Conclusion_

In this example, we've explored how to use the PowerPipe library to build advanced pipelines in a .NET application. By following the steps outlined in the example, you can effectively manage complex data processing tasks with ease. PowerPipe's fluent interface and integration with Microsoft Dependency Injection make it a powerful tool for creating maintainable and reusable pipelines.

By incorporating PowerPipe into your development workflow, you can enhance the efficiency and readability of your code while tackling intricate data processing challenges. As you continue to explore the library and its features, you'll discover even more ways to leverage its capabilities in your projects.

# Contributors are Welcome!

If you have any questions or suggestions, feel free to contact me at m.vorchakov97@gmail.com. I'm welcome contributions from the community to enhance and improve the PowerPipe library. Your input is highly valued!
