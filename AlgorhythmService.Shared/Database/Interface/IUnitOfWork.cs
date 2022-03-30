using System;
using System.Threading.Tasks;

namespace AlgorhythmService.Shared.Database.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        void Commit();
        void Rollback();
    }
}
