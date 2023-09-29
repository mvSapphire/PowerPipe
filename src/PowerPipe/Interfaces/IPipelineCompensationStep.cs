using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipelineCompensationStep<in TContext>
{
    Task CompensateAsync(TContext context) => CompensateAsync(context, default);

    Task CompensateAsync(TContext context, CancellationToken cancellationToken);
}