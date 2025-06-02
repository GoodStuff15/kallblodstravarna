using Microsoft.EntityFrameworkCore;
using resortapi.Data;
using resortlibrary.Models;

namespace resortapi.Repositories
{
    public class AdditionalOptionsRepo : AbstractRepo<AdditionalOption>
    {
        private readonly ResortContext _context;

        public AdditionalOptionsRepo(ResortContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ICollection<AdditionalOption>> GetAllWithIncludesAsync()
        {
            return await _context.Set<AdditionalOption>().ToListAsync();
        }
    }
}
