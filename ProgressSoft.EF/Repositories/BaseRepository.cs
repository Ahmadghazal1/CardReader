using Microsoft.EntityFrameworkCore;
using ProgressSoft.Core.IRepositories;
using ProgressSoft.EF.Data;

namespace ProgressSoft.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            if (model is null)
                return null;
            return model;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
    }
}
