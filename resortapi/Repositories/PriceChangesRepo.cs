using resortapi.Data;
using resortlibrary.Models;

namespace resortapi.Repositories
{
    public class PriceChangesRepo : AbstractRepo<PriceChanges>
    {
        public PriceChangesRepo(ResortContext context) : base(context)
        {
            _context = context;
        }

        public override Task<ICollection<PriceChanges>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
