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
        public void Create(TEntity entity);

        // Read
        public TEntity GetAsync(int id);

        // Update
        public void UpdateAsync(TEntity entity);

        // Delete
        public void DeleteAsync(TEntity entity);


        // Read All 
        public ICollection<TEntity> GetAllAsync();

        
    }
}
