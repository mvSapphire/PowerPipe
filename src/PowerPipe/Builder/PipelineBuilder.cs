using System;
using System.Collections.Generic;
using PowerPipe.Builder.Steps;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder;

public sealed class PipelineBuilder<TContext, TResult>
    where TContext : PipelineContext<TResult>
    where TResult : class
{
    private readonly List<IPipelineStep<TContext>> _steps = new();

    private readonly IPipelineStepFactory _pipelineStepFactory;
    private readonly TContext _context;

    public PipelineBuilder(IPipelineStepFactory pipelineStepFactory, TContext context)
    {
        _ = pipelineStepFactory ?? throw new ArgumentNullException(nameof(pipelineStepFactory));
        _ = context ?? throw new ArgumentNullException(nameof(context));

        _pipelineStepFactory = pipelineStepFactory;
        _context = context;
    }

    public PipelineBuilder<TContext, TResult> Add<T>()
        where T : IPipelineStep<TContext>
    {
        _steps.Add(new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>));

        return this;
    }

    public PipelineBuilder<TContext, TResult> AddWhen<T>(Predicate<TContext> predicate)
        where T : IPipelineStep<TContext>
    {
        _steps.Add(new AddWhenStep<TContext>(predicate, new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>)));

        return this;
    }

    public PipelineBuilder<TContext, TResult> When(Func<TContext, bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
    {
        return When(() => predicate(_context), action);
    }

    public PipelineBuilder<TContext, TResult> When(Func<bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
    {
        var whenBuilder = action(new PipelineBuilder<TContext, TResult>(_pipelineStepFactory, _context));

        _steps.Add(new WhenPipelineStep<TContext, TResult>(predicate, whenBuilder));

        return this;
    }

    public IPipeline<TResult> Build()
    {
        _steps.Add(new FinishStep<TContext>());

        return new Pipeline<TContext, TResult>(_context, _steps);
    }
}