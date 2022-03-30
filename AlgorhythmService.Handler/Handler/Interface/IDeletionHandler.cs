using System.Threading;
using System.Threading.Tasks;

namespace AlgorhythmService.Handler.Handler.Interface
{
    public interface IDeletionHandler
    {
        Task DeleteOldDataAsync(CancellationToken stoppingToken);
    }
}
