using System;
using System.Collections.Generic;
using PowerPipe.Builder.Steps;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder;

/// <summary>
/// Represents a builder for creating a pipeline of steps.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
/// <typeparam name="TResult">The type of result returned by the pipeline.</typeparam>
public sealed class PipelineBuilder<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly IPipelineStepFactory _pipelineStepFactory;
    private volatile TContext _context;

    internal List<InternalStep<TContext>> Steps { get; } = new();
    private InternalStep<TContext> LastStep => Steps[^1];

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineBuilder{TContext, TResult}"/> class.
    /// </summary>
    /// <param name="pipelineStepFactory">The factory for creating pipeline steps.</param>
    /// <param name="context">The pipeline context.</param>
    public PipelineBuilder(IPipelineStepFactory pipelineStepFactory, TContext context)
    {
        ArgumentNullException.ThrowIfNull(pipelineStepFactory);
        ArgumentNullException.ThrowIfNull(context);

        _pipelineStepFactory = pipelineStepFactory;
        _context = context;
    }

    /// <summary>
    /// Adds a step to the pipeline.
    /// </summary>
    /// <typeparam name="T">The type of step to add.</typeparam>
    /// <returns>The current instance of the pipeline builder.</returns>
    public PipelineBuilder<TContext, TResult> Add<T>()
        where T : IStepBase<TContext>
    {
        Steps.Add(new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>));

        return this;
    }

    /// <summary>
    /// Adds a conditional step to the pipeline.
    /// </summary>
    /// <typeparam name="T">The type of step to add conditionally.</typeparam>
    /// <param name="predicate">The predicate to determine whether to execute the step.</param>
    /// <returns>The current instance of the pipeline builder.</returns>
    public PipelineBuilder<TContext, TResult> AddIf<T>(Predicate<TContext> predicate)
        where T : IStepBase<TContext>
    {
        Steps.Add(new AddIfStep<TContext>(predicate, new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>)));

        return this;
    }

    /// <summary>
    /// Adds an if-else conditional step to the pipeline.
    /// </summary>
    /// <typeparam name="TIf">The type of step to add if the predicate is true.</typeparam>
    /// <typeparam name="TElse">The type of step to add if the predicate is false.</typeparam>
    /// <param name="predicate">The predicate to determine whether to execute the if step.</param>
    /// <returns>The current instance of the pipeline builder.</returns>
    public PipelineBuilder<TContext, TResult> AddIfElse<TIf, TElse>(Predicate<TContext> predicate)
        where TIf : IStepBase<TContext>
        where TElse : IStepBase<TContext>
    {
        Steps.Add(new AddIfElseStep<TContext>(
            predicate,
            new LazyStep<TContext>(_pipelineStepFactory.Create<TIf, TContext>),
            new LazyStep<TContext>(_pipelineStepFactory.Create<TElse, TContext>)));

        return this;
    }

    /// <summary>
    /// Adds a conditional branch to the pipeline.
    /// </summary>
    /// <param name="predicate">The predicate to determine whether to execute the branch.</param>
    /// <param name="action">An action to configure the branch.</param>
    /// <returns>The current instance of the pipeline builder.</returns>
    public PipelineBuilder<TContext, TResult> If(
        Predicate<TContext> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
    {
        var internalBuilder = action(new PipelineBuilder<TContext, TResult>(_pipelineStepFactory, _context));

        Steps.Add(new IfPipelineStep<TContext, TResult>(predicate, internalBuilder));

        return this;
    }

    /// <summary>
    /// Adds a parallel execution branch to the pipeline.
    /// </summary>
    /// <param name="action">An action to configure the parallel execution branch.</param>
    /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism for the branch.</param>
    /// <returns>The current instance of the pipeline builder.</returns>
    public PipelineBuilder<TContext, TResult> Parallel(
        Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action, int maxDegreeOfParallelism = -1)
    {
        var internalBuilder = action(new PipelineBuilder<TContext, TResult>(_pipelineStepFactory, _context));

        Steps.Add(new ParallelStep<TContext, TResult>(maxDegreeOfParallelism, internalBuilder));

        return this;
    }

    /// <summary>
    /// Configures error handling for the last added step.
    /// </summary>
    /// <param name="errorHandling">The error handling behavior to configure.</param>
    /// <param name="retryInterval">The retry interval for retrying the step in case of an error.</param>
    /// <param name="maxRetryCount">The maximum number of times to retry the step.</param>
    /// <param name="predicate">A predicate to determine whether error handling should be applied.</param>
    /// <returns>The current instance of the pipeline builder.</returns>
    public PipelineBuilder<TContext, TResult> OnError(PipelineStepErrorHandling errorHandling,
        TimeSpan? retryInterval = null, int? maxRetryCount = null, Predicate<TContext> predicate = null)
    {
        LastStep.ConfigureErrorHandling(errorHandling, retryInterval, maxRetryCount, predicate);

        return this;
    }

    /// <summary>
    /// Adds a compensation step to the last added step in the pipeline.
    /// </summary>
    /// <typeparam name="T">The type of compensation step to add.</typeparam>
    /// <returns>The current instance of the pipeline builder.</returns>
    public PipelineBuilder<TContext, TResult> CompensateWith<T>()
        where T : IPipelineCompensationStep<TContext>
    {
        LastStep.CompensationStep = new CompensationStep<TContext>(_pipelineStepFactory.CreateCompensation<T, TContext>);

        return this;
    }

    /// <summary>
    /// Builds the pipeline based on the configured steps.
    /// </summary>
    /// <returns>The constructed pipeline.</returns>
    public IPipeline<TResult> Build()
    {
        Steps.Add(new FinishStep<TContext>());

        return new Pipeline<TContext, TResult>(_context, Steps);
    }
}