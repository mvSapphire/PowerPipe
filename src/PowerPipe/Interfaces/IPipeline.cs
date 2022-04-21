using System;
using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipeline<TContext>
    where TContext : PipelineContext<Type>
{
    Task<Type> RunAsync(bool returnResult = true);
}