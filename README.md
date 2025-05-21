<p align="center">
  <img src="https://github.com/mvSapphire/PowerPipe/blob/master/assets/readme-header.png?raw=true" alt="drawing" width="250"/>
</p>

<span align="center">

[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/mvSapphire/PowerPipe)

</span>

<span align="center">

[![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/mvSapphire/PowerPipe/build.yml)](https://github.com/mvSapphire/PowerPipe/actions)
![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/mvSapphire/87b833b49852f7f088e1d4d913600a91/raw/code-coverage.json)
[![Nuget](https://img.shields.io/nuget/v/PowerPipe)](https://www.nuget.org/packages/PowerPipe)
[![Nuget](https://img.shields.io/nuget/dt/PowerPipe)](https://www.nuget.org/stats/packages/PowerPipe?groupby=Version)

</span>

# A .NET Library for Constructing Advanced Workflows with Fluent Interface

PowerPipe is a versatile .NET library designed to streamline the process of building advanced workflows using a fluent interface. The primary objective of this project is to eliminate the need for writing boilerplate code when implementing workflows.

Check out [Medium article](https://medium.com/@m.vorchakov97/from-chaos-to-clarity-enhance-data-processing-with-powerpipe-in-net-262ac34a4923) 👀

If you like this project give it a star 🌟

## 🔥 Features and Benefits

- Lightweight
- Fluent interface
- Conditional steps
- Parallel steps execution
- Nested pipelines
- Error handling
- Steps compensation
- Ease & Structured Workflow construction
- Dependency Injection support
- Developed using latest .NET

## 🧐 Sample use case

Imagine creating an e-commerce platform. The platform must process incoming customer orders, each demanding validation, inventory updates, and potentially more intricate steps.

```csharp
public  class  ECommercePipelineService : IECommercePipelineService
{
    private  readonly  IPipelineStepFactory _pipelineStepFactory;

    private bool PaymentSucceed(ECommerceContext context) => context.PaymentResult.Status is PaymentStatus.Success;

    public  ECommercePipelineService(IPipelineStepFactory pipelineStepFactory)
    {
        _pipelineStepFactory  =  pipelineStepFactory;
    }

    public IPipeline<OrderResult> BuildPipeline()
    {
        var context = new ECommerceContext();

        return new PipelineBuilder<ECommerceContext, OrderResult>(_pipelineStepFactory, context)
            .Add<OrderValidationStep>()
            .Add<PaymentProcessingStep>()
                .OnError(PipelineStepErrorHandling.Retry,  retryInterval:  TimeSpan.FromSeconds(2), maxRetryCount: 2)
            .If(PaymentSucceed, b => b
                .Add<OrderConfirmationStep>()
                .Add<InventoryReservationStep>())
            .Parallel(b => b
                .Add<CustomerNotificationsStep>()
                .Add<AnalyticsAndReportingStep>(), maxDegreeOfParallelism: 2)
            .Build();
    }
}
```

## 🤩 Workflow visualization

Sometimes workflows could be too big to track what is happening.

That's why we created a workflow visualization tool. This tool was designed with simplicity in mind.
You have to add just **few lines of code** to make this work!

### Install required packages

- Package Manager Console
```
Install-Package PowerPipe.Visualization
Install-Package PowerPipe.Visualization.Extensions.MicrosoftDependencyInjection
```

- .NET CLI
```
dotnet add package PowerPipe.Visualization
dotnet add package PowerPipe.Visualization.Extensions.MicrosoftDependencyInjection
```

### Usage

In your Startup/Program file add required services and register middleware:

``` csharp
builder.Services.AddPowerPipeVisualization(o => o.ScanFromType(typeof(ECommercePipelineService)));

// ...

app.UsePowerPipeVisualization();
```

Then start you application and navigate to `/powerpipe` endpoint.

And workflow from the sample above parsed to this beautiful diagram. 🌟

<img src="https://github.com/mvSapphire/PowerPipe/blob/master/assets/readme-diagram-sample.png?raw=true" alt="drawing" width="800"/>

> Note! This is the very first version of workflow visualization! Looking forward to your feedback 🤗

> Known issues: 
> - OnError parsing could lead to missing steps on the diagram
> - Double links between entities

## 🛠️ Getting started

### Installation

- Package Manager Console
```
Install-Package PowerPipe
Install-Package PowerPipe.Extensions.MicrosoftDependencyInjection
```

- .NET CLI
```
dotnet add package PowerPipe
dotnet add package PowerPipe.Extensions.MicrosoftDependencyInjection
```

###  Building pipeline

1. **Create pipeline context and result**

```csharp
public class SampleContext : PipelineContext<SampleResult>  
{  
    // Properties and methods specific to the context  
}

public class SampleResult
{
    // Implementation details
}
```

2. **Create pipeline steps**

```csharp
public class SampleStep1 : IPipelineStep<SampleContext>  
{  
    // Implementation details…
}
  
public  class  SampleStep2 : IPipelineStep<OrderContext>  
{  
    // Implementation details…
}
```

3. **Define your pipeline**

- Use `Add<T>` method to add a step to your pipeline

```csharp
var pipeline = new PipelineBuilder<OrderProcessingContext, Order>()
    .Add<SampleStep1>()
    .Add<SampleStep2>()
    .Build();
```

- Use `AddIf<T>` method to add a step to the pipeline based on the predicate 

```csharp
// Define predicate based on context
private bool ExecuteStep2(OrderProcessingContext context) =>
    context.ExecuteStep2Allowed;

var pipeline = new PipelineBuilder<OrderProcessingContext, Order>()
    .Add<SampleStep1>()
    .AddIf<SampleStep2>(ExecuteStep2) 
    .Build();
```

- Use `AddIfElse<TFirst, TSecond>` method to add one of the steps by the predicate

```csharp
private bool ExecuteStep2(OrderProcessingContext context) =>
    context.ExecuteStep2Allowed;

var pipeline = new PipelineBuilder<OrderProcessingContext, Order>()
    .AddIfElse<SampleStep1, SampleStep2>(ExecuteStep2) 
    .Build();
```

- Use `If` method to add a nested pipeline based on a predicate

```csharp
private bool ExecuteNestedPipeline(OrderProcessingContext context) =>
    context.ExecuteNestedPipelineAllowed;

var pipeline = new PipelineBuilder<OrderProcessingContext, Order>()
    .If(ExecuteNestedPipeline, b => b
        .Add<SampleStep1>()
        .Add<SampleStep2>())
    .Build();
```

- Use `Parallel` method to execute your steps in parallel

> In order to execute steps in parallel your steps should implement `IPipelineParallelStep<TContext>` interface

```csharp
var pipeline = new PipelineBuilder<OrderProcessingContext, Order>()
    .Parallel(b => b  
        .Add<SampleParallelStep1>()
        .Add<SampleParallelStep2>(), maxDegreeOfParallelism: 3)
    .Build();
```

- Use `OnError` method to add error-handling behavior

> Currently available only two types of error handling `Suppress` and `Retry`

```csharp
var pipeline = new PipelineBuilder<OrderProcessingContext, Order>()
    .Add<SampleStep1>()
        .OnError(PipelineStepErrorHandling.Retry)
    .Build();
```

- Use `CompensateWith` method to add a compensation step to the previously added step in the pipeline

> Compensation steps should implement `IPipelineCompensationStep<TContext>`

```csharp
public class SampleStep1Compensation : IPipelineCompensationStep<SampleContext> {}

var pipeline = new PipelineBuilder<OrderProcessingContext, Order>()
    .Add<SampleStep1>()
        .CompensateWith<SampleStep1Compensation>()
    .Build();
```

4. **Extensions: Microsoft Dependency Injection**

The `PowerPipe.Extensions.MicrosoftDependencyInjection` extension provides integration with Microsoft Dependency Injection.

- Use `AddPowerPipe` to register all required services and scan libraries for your step implementations.

```csharp
public static IServiceCollection AddPowerPipe(
    this IServiceCollection serviceCollection,
    PowerPipeConfiguration configuration)
```

By default all found implementations will be registered as Transient.

```csharp
services.AddPowerPipe(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});
```

But you can configure service lifetime per step implementation.

```csharp
services.AddPowerPipe(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly)
        .ChangeStepsDefaultLifetime(ServiceLifetime.Scoped)
        .AddSingleton<Step1>()
        .AddTransient<Step2>()
        .AddTransient(typeof(Step2));
});
```


Check out [sample project](samples/PowerPipe.Sample) 👀
