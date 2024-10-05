using ProgressSoft.Core.Entites;
using ProgressSoft.Core.IRepositories;

namespace ProgressSoft.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<CardReader> CardReaders { get; }
        Task CompleteAsync();
    }
}
