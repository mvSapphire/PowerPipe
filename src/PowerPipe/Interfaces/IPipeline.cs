using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

public interface IPipeline<TResult>
{
    Task<TResult> RunAsync(bool returnResult = true);
}