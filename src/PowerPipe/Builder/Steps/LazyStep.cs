﻿using System;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

public class LazyStep<TContext> : IPipelineStep<TContext>
    where TContext : PipelineContext
{
    private readonly Lazy<IPipelineStep<TContext>> _step;
    
    internal LazyStep(Func<IPipelineStep<TContext>> factory)
    {
        _step = new Lazy<IPipelineStep<TContext>>(() =>
        {
            var instance = factory();
            instance.NextStep = NextStep;
            return instance;
        });
    }

    public IPipelineStep<TContext> NextStep { get; set; }
    public Task ExecuteAsync(TContext context)
    {
        throw new NotImplementedException();
    }
}