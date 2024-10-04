using ProgressSoft.Core;
using ProgressSoft.Core.Entites;
using ProgressSoft.Core.IRepositories;
using ProgressSoft.EF.Data;
using ProgressSoft.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressSoft.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<CardRedaer> CardReaders { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {

            _context = context;
            CardReaders = new BaseRepository<CardRedaer>(_context);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
