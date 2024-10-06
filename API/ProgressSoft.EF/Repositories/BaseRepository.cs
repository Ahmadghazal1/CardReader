using Microsoft.EntityFrameworkCore;
using ProgressSoft.Core.Helper;
using ProgressSoft.Core.IRepositories;
using ProgressSoft.EF.Data;
using System.Linq.Expressions;

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

        public async Task<T?> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            return entity;
        }

        public async Task<List<T>> CreateRaneAsync(List<T> entites)
        {
           await _context.Set<T>().AddRangeAsync(entites);
            return entites;
        }
    }
}
