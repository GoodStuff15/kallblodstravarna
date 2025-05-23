using resortapi.Data;
using resortlibrary.Models;

namespace resortapi.Repositories
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
