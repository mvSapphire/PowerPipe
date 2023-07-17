using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipeline<TResult>
{
    Task<TResult> RunAsync(bool returnResult = true) => RunAsync(default, returnResult);

    Task<TResult> RunAsync(CancellationToken cancellationToken, bool returnResult = true);
}