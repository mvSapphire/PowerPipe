using System;
using System.Collections.Generic;
using PowerPipe.Builder.Steps;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder;

public sealed class PipelineBuilder
{
    private readonly List<IPipelineStep> _steps = new();

    private readonly IPipelineStepFactory _pipelineStepFactory;
    private readonly PipelineContext _context;

    public PipelineBuilder(IPipelineStepFactory pipelineStepFactory, PipelineContext context)
    {
        _pipelineStepFactory = pipelineStepFactory;
        _context = context;
    }

    public PipelineBuilder Add<T>()
        where T : IPipelineStep
    {
        _steps.Add(new LazyStep(_pipelineStepFactory.Create<T>));

        return this;
    }

    public PipelineBuilder AddWhen<T>(Predicate<PipelineContext> predicate)
        where T : IPipelineStep
    {
        _steps.Add(new AddWhenStep(predicate, new LazyStep(_pipelineStepFactory.Create<T>)));

        return this;
    }

    public PipelineBuilder When(Func<PipelineContext, bool> predicate, Func<PipelineBuilder, PipelineBuilder> action)
    {
        return When(() => predicate(_context), action);
    }

    public PipelineBuilder When(Func<bool> predicate, Func<PipelineBuilder, PipelineBuilder> action)
    {
        var whenBuilder = action(new PipelineBuilder(_pipelineStepFactory, _context));

        _steps.Add(new WhenPipelineStep(predicate, whenBuilder));

        return this;
    }

    public IPipeline Build()
    {
        _steps.Add(new FinishStep());

        return new Pipeline(_context, _steps);
    }
}