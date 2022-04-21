using System;
using System.Collections.Generic;
using PowerPipe.Builder.Steps;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder;

public sealed class PipelineBuilder<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly List<IPipelineStep<TContext, TResult>> _steps = new();

    private readonly IPipelineStepFactory _pipelineStepFactory;
    private readonly TContext _context;

    public PipelineBuilder(IPipelineStepFactory pipelineStepFactory, TContext context)
    {
        _pipelineStepFactory = pipelineStepFactory;
        _context = context;
    }

    public PipelineBuilder<TContext, TResult> Add<T>()
        where T : IPipelineStep<TContext, TResult>
    {
        _steps.Add(new LazyStep<TContext, TResult>(_pipelineStepFactory.Create<T, TContext, TResult>));

        return this;
    }

    public PipelineBuilder<TContext, TResult> AddWhen<T>(Predicate<PipelineContext<TResult>> predicate)
        where T : IPipelineStep<TContext, TResult>
    {
        _steps.Add(new AddWhenStep<TContext, TResult>(predicate, new LazyStep<TContext, TResult>(_pipelineStepFactory.Create<T, TContext, TResult>)));

        return this;
    }

    public PipelineBuilder<TContext, TResult> When(Func<PipelineContext<TResult>, bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
    {
        return When(() => predicate(_context), action);
    }

    public PipelineBuilder<TContext, TResult> When(Func<bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
    {
        var whenBuilder = action(new PipelineBuilder<TContext, TResult>(_pipelineStepFactory, _context));

        _steps.Add(new WhenPipelineStep<TContext, TResult>(predicate, whenBuilder));

        return this;
    }

    public IPipeline<TContext, TResult> Build()
    {
        _steps.Add(new FinishStep<TContext, TResult>());

        return new Pipeline<TContext, TResult>(_context, _steps);
    }
}