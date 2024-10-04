using ProgressSoft.Core.Entites;
using ProgressSoft.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressSoft.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<CardRedaer> CardReaders { get; }
        Task CompleteAsync();
    }
}
