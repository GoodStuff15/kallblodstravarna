using Microsoft.EntityFrameworkCore;
using resortapi.Data;

namespace resortapi.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ResortContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ResortContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public Task<ICollection<T>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
