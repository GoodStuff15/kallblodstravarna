using resortapi.Data;
using resortlibrary.Models;

namespace resortapi.Repositories
{
    public class AccomodationTypeRepo : AbstractRepo<AccomodationType>
    {
        public AccomodationTypeRepo(ResortContext context) : base(context)
        {
            _context = context;
        }

        public override Task<ICollection<AccomodationType>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
