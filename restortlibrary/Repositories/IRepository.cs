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
        public TEntity Get(int id);

        // Update
        public void Update(TEntity entity);

        // Delete
        public void Delete(TEntity entity);


        // Read All 
        public ICollection<TEntity> GetAll();

        
    }
}
