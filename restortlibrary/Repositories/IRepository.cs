using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
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

        // Read All With Includes
        public  Task<ICollection<TEntity>> GetAllWithIncludesAsync();

    }
}
