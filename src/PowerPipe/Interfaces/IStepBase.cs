using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IStepBase<in TContext>
{
    Task ExecuteAsync(TContext context) => ExecuteAsync(context, default);

    Task ExecuteAsync(TContext context, CancellationToken cancellationToken);
}