using System;
using System.Collections.Generic;
using PowerPipe.Builder.Steps;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder;

public sealed class PipelineBuilder<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly IPipelineStepFactory _pipelineStepFactory;
    private volatile TContext _context;

    internal List<InternalStep<TContext>> Steps { get; } = new();
    private InternalStep<TContext> LastStep => Steps[^1];

    public PipelineBuilder(IPipelineStepFactory pipelineStepFactory, TContext context)
    {
        ArgumentNullException.ThrowIfNull(pipelineStepFactory);
        ArgumentNullException.ThrowIfNull(context);

        _pipelineStepFactory = pipelineStepFactory;
        _context = context;
    }

    public PipelineBuilder<TContext, TResult> Add<T>()
        where T : IStepBase<TContext>
    {
        Steps.Add(new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>));

        return this;
    }

    public PipelineBuilder<TContext, TResult> AddIf<T>(Predicate<TContext> predicate)
        where T : IStepBase<TContext>
    {
        Steps.Add(new AddIfStep<TContext>(predicate, new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>)));

        return this;
    }

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

    public PipelineBuilder<TContext, TResult> If(
        Predicate<TContext> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
    {
        var internalBuilder = action(new PipelineBuilder<TContext, TResult>(_pipelineStepFactory, _context));

        Steps.Add(new IfPipelineStep<TContext, TResult>(predicate, internalBuilder));

        return this;
    }

    public PipelineBuilder<TContext, TResult> Parallel(
        Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action, int maxDegreeOfParallelism = -1)
    {
        var internalBuilder = action(new PipelineBuilder<TContext, TResult>(_pipelineStepFactory, _context));

        Steps.Add(new ParallelStep<TContext, TResult>(maxDegreeOfParallelism, internalBuilder));

        return this;
    }

    public PipelineBuilder<TContext, TResult> OnError(PipelineStepErrorHandling errorHandling,
        TimeSpan? retryInterval = null, int? maxRetryCount = null, Predicate<TContext> predicate = null)
    {
        LastStep.ConfigureErrorHandling(errorHandling, retryInterval, maxRetryCount, predicate);

        return this;
    }

    public PipelineBuilder<TContext, TResult> CompensateWith<T>()
        where T : IPipelineCompensationStep<TContext>
    {
        LastStep.CompensationStep = new CompensationStep<TContext>(_pipelineStepFactory.CreateCompensation<T, TContext>);

        return this;
    }

    public IPipeline<TResult> Build()
    {
        Steps.Add(new FinishStep<TContext>());

        return new Pipeline<TContext, TResult>(_context, Steps);
    }
}