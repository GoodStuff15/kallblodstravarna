using restortlibrary.Data;
using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Repositories
{
    public class CustomerRepo : AbstractRepo<Customer>
    {
        public CustomerRepo(ResortContext context) : base(context) 
        {
            _context = context;
        }

        public override Task<ICollection<Customer>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
