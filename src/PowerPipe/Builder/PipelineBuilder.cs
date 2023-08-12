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

    public PipelineBuilder<TContext, TResult> AddIf<T>(Predicate<TContext> predicate)
        where T : IPipelineStep<TContext>
    {
        _steps.Add(new AddIfStep<TContext>(predicate, new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>)));

        return this;
    }

    public PipelineBuilder<TContext, TResult> AddIfElse<TIf, TElse>(Predicate<TContext> predicate)
        where TIf : IPipelineStep<TContext>
        where TElse : IPipelineStep<TContext>
    {
        _steps.Add(new AddIfElseStep<TContext>(
            predicate,
            new LazyStep<TContext>(_pipelineStepFactory.Create<TIf, TContext>),
            new LazyStep<TContext>(_pipelineStepFactory.Create<TElse, TContext>)));

        return this;
    }

    public PipelineBuilder<TContext, TResult> If(
        Func<TContext, bool> predicate,
        Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action) => If(() => predicate(_context), action);

    public PipelineBuilder<TContext, TResult> If(Func<bool> predicate, Func<PipelineBuilder<TContext, TResult>, PipelineBuilder<TContext, TResult>> action)
    {
        var whenBuilder = action(new PipelineBuilder<TContext, TResult>(_pipelineStepFactory, _context));

        _steps.Add(new IfPipelineStep<TContext, TResult>(predicate, whenBuilder));

        return this;
    }

    public IPipeline<TResult> Build()
    {
        _steps.Add(new FinishStep<TContext>());

        return new Pipeline<TContext, TResult>(_context, _steps);
    }
}