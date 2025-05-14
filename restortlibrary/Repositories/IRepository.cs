using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Repositories
{
    public interface IRepository<T>
    {
        // Create
        public Task<T> CreateAsync(T t);

        // Read
        public Task<T> GetAsync(int id);

        // Update
        public Task<T> UpdateAsync(T t);

        // Delete
        public Task DeleteAsync(int id);


        // Read All 
        public ICollection<T> GetAllAsync();

        
    }
}
