using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Repositories
{
    public interface IRepository<TEntity>
    {
        // Create
        public Task CreateAsync(TEntity entity);

        // Read
        public Task<TEntity> GetAsync(int id);

        // Update
        public Task UpdateAsync(TEntity entity);

        // Delete
        public Task DeleteAsync(TEntity entity);


        // Read All 
        public Task<ICollection<TEntity>> GetAllAsync();

        
    }
}
