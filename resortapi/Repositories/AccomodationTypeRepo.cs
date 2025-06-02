using Microsoft.EntityFrameworkCore;
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
        public async Task<AccomodationType?> GetByIdAsync(int id)
        {
            return await _context.AccomodationTypes
                .Include(a => a.Accomodations)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AccomodationType?> AddAsync(AccomodationType accomodationType)
        {
            _context.AccomodationTypes.Add(accomodationType);
            await _context.SaveChangesAsync();
            return accomodationType;
        }
        public async Task<AccomodationType?> UpdateAsync(AccomodationType accomodationType)
        {
            _context.AccomodationTypes.Update(accomodationType);
            await _context.SaveChangesAsync();
            return accomodationType;
        }
    }
}
