using resortapi.Data;
using resortlibrary.Models;

namespace resortapi.Repositories
{
    public class AdditionalOptionsRepo : AbstractRepo<AdditionalOption>
    {
        public AdditionalOptionsRepo(ResortContext context) : base(context)
        {
            _context = context;
        }

        public override Task<ICollection<AdditionalOption>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
