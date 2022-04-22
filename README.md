# PowerPipe
A library for .NET that uses a fluent interface to construct advanced pipelines with ease.

The idea of this project to get you rid of writing boilerplate code to implement pipeline.

- Written in .NET 6
- Readable and easy to use

[PowerPipe NugetPackage](https://www.nuget.org/packages/PowerPipe/ "PowerPipe NugetPackage")
[PowerPipe.Extensions.MicrosoftDependencyInjection NugetPackage](https://www.nuget.org/packages/PowerPipe.Extensions.MicrosoftDependencyInjection/ "PowerPipe.Extensions.MicrosoftDependencyInjection NugetPackage")

## How to use
Just install the following packages:
```csharp
Install-Package PowerPipe
Install-Package PowerPipe.Extensions.MicrosoftDependencyInjection
```
or with .NET CLI
```csharp
dotnet add package PowerPipe
dotnet add package PowerPipe.Extensions.MicrosoftDependencyInjection
```

------------

Lets see what the base entities you will work with.

### PipelineContext
Generic representation of abstract context pipeline class from which all your contexts should be derived.

```csharp
public abstract class PipelineContext<TResult>
	where TResult : class
```

#### Methods
```csharp
public abstract TResult GetPipelineResult();
```
Returns result of pipeline type of `TResult`

------------

### Pipeline
Generic representation of pipeline. Probably you will never instantiate this by yourself. The main goal of this class is to connect all the steps under the hood and execute the first step which will start the chain.
```csharp
public class Pipeline<TContext, TResult> : IPipeline<TResult>
	where TContext : PipelineContext<TResult>
	where TResult : class
```
Implements `public interface IPipeline<TResult>`

#### Methods
##### RunAsync

```csharp
public async Task<TResult> RunAsync(bool returnResult = true)
```
The only method you are able to call. Retrieves `returnResult` param which is true by default; you can use this if you don't need the result of the pipeline and this param is `false` for nested pipelines (about that later).

------------

### Steps
#### IPipelineStep
First of all, there is an generic interface that you should implement to describe your own pipeline steps.
```csharp
public interface IPipelineStep<TContext>
```
##### Properties
```csharp
public IPipelineStep<TContext> NextStep { get; set; }
```
Represents the next pipeline step.
##### Methods
###### ExecuteAsync

```csharp
Task ExecuteAsync(TContext context);
```
The main method which will be called under the hood. There you should implement your logic.

------------

**Steps that are implemented and called under the hood but that you should be aware of**

#### LazyStep
```csharp
public class LazyStep<TContext> : IPipelineStep<TContext>
```
This class's like a 'decorator' of your steps to make them threadsafe

#### AddWhenStep
```csharp
internal class AddWhenStep<TContext> : IPipelineStep<TContext>
```
The goal of this class is to add your step to the main pipeline by some predicate.

#### WhenPipelineStep
```csharp
public class WhenPipelineStep<TContext, TResult> : IPipelineStep<TContext>
    where TContext : PipelineContext<TResult>
    where TResult : class
```
This is the 'nested pipeline' which I mentioned earlier. This step adds nested pipeline by some predicate.

#### FinishStep
```csharp
public class FinishStep<TContext> : IPipelineStep<TContext>
```
Nothing to describe actually :) Adds automatically as the last step of the pipeline.

------------

### PipelineStepFactory
```csharp
public class PipelineStepFactory : IPipelineStepFactory
```
Implements `IPipelineStepFactory` that obtains steps from DI.

> Yes, you can inject everything you want to your steps ;)

------------

### PipelineBuilder
Represents the main object to build your pipeline.
```csharp
public sealed class PipelineBuilder<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
```
Hope you already aware what is `TContext` and `TResult`.

#### Methods
##### Add
```csharp
public PipelineBuilder<TContext, TResult> Add<T>()
        where T : IPipelineStep<TContext>
```
Just adds your step to the end of pipeline.

##### AddWhen
```csharp
public PipelineBuilder<TContext, TResult> AddWhen<T>(Predicate<TContext> predicate)
        where T : IPipelineStep<TContext>
```
Adds your step on some predicate.

##### When
```csharp
public PipelineBuilder<TContext, TResult> When(Func<bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
```
Adds nested pipeline on some predicate.

##### When (but another predicate (: )
```csharp
public PipelineBuilder<TContext, TResult> When(Func<TContext, bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
```
Actually alike as on above but obtain `TContext` predicate.

##### Build
```csharp
public IPipeline<TResult> Build()
```
Adds `FinishStep<TContext>` and return `IPipeline<TResult>`.

### Extensions
#### PowerPipe.Extensions.MicrosoftDependencyInjection
MS Dependency Injection extensions.

#### Methods
##### AddPowerPipe
```csharp
public static IServiceCollection AddPowerPipe(this IServiceCollection serviceCollection)
```
To add `IPipelineStepFactory` to DI.

##### AddPowerPipeStep
```csharp
public static IServiceCollection AddPowerPipeStep<TStep, TContext>(this IServiceCollection serviceCollection)
        where TStep : class, IPipelineStep<TContext>
        where TContext : PipelineContext<Type>
```
Add your steps to DI as `Transient`.

### Examples
Comming soon! :)


# Contributors are welcome!
#### Contact if you have any questions m.vorchakov97@gmail.com