using Microsoft.EntityFrameworkCore;
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
        public async Task<PriceChanges?> GetByIdAsync(int id)
        {
            return await _context.PriceChanges
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PriceChanges?> AddAsync(PriceChanges priceChanges)
        {
            _context.PriceChanges.Add(priceChanges);
            await _context.SaveChangesAsync();
            return priceChanges;
        }
        public async Task<PriceChanges?> UpdateAsync(PriceChanges priceChanges)
        {
            _context.PriceChanges.Update(priceChanges);
            await _context.SaveChangesAsync();
            return priceChanges;
        }
        public async Task DeleteAsync(PriceChanges priceChanges)
        {
            _context.Set<PriceChanges>().Remove(priceChanges);
            await _context.SaveChangesAsync();
        }
    }
}
