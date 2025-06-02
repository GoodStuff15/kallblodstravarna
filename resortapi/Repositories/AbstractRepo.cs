using Microsoft.EntityFrameworkCore;
using resortapi.Data;

namespace resortapi.Repositories
{
    public abstract class AbstractRepo<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        protected ResortContext _context;

        public AbstractRepo(ResortContext context)
        {
            _context = context;
        }

        // Create
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            await _context.SaveChangesAsync();

        }

        // Read
        public async Task<TEntity> GetAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            return entity;
        }

        // Update
        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        // Read All
        public async Task<ICollection<TEntity>> GetAllAsync()
        {

            var all = from t in _context.Set<TEntity>()
                      select t;
                     

            return await all.ToListAsync();
        }

        // Read all with includes

        public abstract Task<ICollection<TEntity>> GetAllWithIncludesAsync();

    }
}
