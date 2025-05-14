using restortlibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Repositories
{
    public class AbstractRepo<TEntity> where TEntity : class
    {
        protected ResortContext _context;

        protected AbstractRepo(ResortContext context)
        {
            _context = context;
        }

        // Create
        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        // Read
        public TEntity Get(int id)
        {
            var entity = _context.Set<TEntity>().FindAsync(id);

            return entity.Result;
        }

        // Update
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        // Delete
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        // Read All
        public ICollection<TEntity> GetAll()
        {
            return  _context.Set<TEntity>().ToList();
        }

    }
}
