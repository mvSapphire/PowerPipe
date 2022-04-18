using System;
using System.Collections.Generic;
using PowerPipe.Builder.Steps;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder;

public sealed class PipelineBuilder<TContext>
    where TContext : PipelineContext
{
    private readonly List<IPipelineStep<TContext>> _steps = new();

    private readonly IPipelineStepFactory _pipelineStepFactory;
    private readonly TContext _context;

    public PipelineBuilder(IPipelineStepFactory pipelineStepFactory, TContext context)
    {
        _pipelineStepFactory = pipelineStepFactory;
        _context = context;
    }

    public PipelineBuilder<TContext> Add<T>()
        where T : IPipelineStep<TContext>
    {
        _steps.Add(new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>));

        return this;
    }

    public PipelineBuilder<TContext> AddWhen<T>(Predicate<PipelineContext> predicate)
        where T : IPipelineStep<TContext>
    {
        _steps.Add(new AddWhenStep<TContext>(predicate, new LazyStep<TContext>(_pipelineStepFactory.Create<T, TContext>)));

        return this;
    }

    public PipelineBuilder<TContext> When(Func<PipelineContext, bool> predicate, Func<PipelineBuilder<TContext>, PipelineBuilder<TContext>> action)
    {
        return When(() => predicate(_context), action);
    }

    public PipelineBuilder<TContext> When(Func<bool> predicate, Func<PipelineBuilder<TContext>, PipelineBuilder<TContext>> action)
    {
        var whenBuilder = action(new PipelineBuilder<TContext>(_pipelineStepFactory, _context));

        _steps.Add(new WhenPipelineStep<TContext>(predicate, whenBuilder));

        return this;
    }

    public IPipeline<TContext> Build()
    {
        _steps.Add(new FinishStep<TContext>());

        return new Pipeline<TContext>(_context, _steps);
    }
}