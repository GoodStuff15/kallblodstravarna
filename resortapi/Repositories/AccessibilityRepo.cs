using Microsoft.EntityFrameworkCore;
using resortapi.Data;
using resortlibrary.Models;

namespace resortapi.Repositories
{
    public class AccessibilityRepo : AbstractRepo<Accessibility>
    {
        public AccessibilityRepo(ResortContext context) : base(context)
        {
            _context = context;
        }
        public override Task<ICollection<Accessibility>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<Accessibility?> GetByIdAsync(int id)
        {
            return await _context.Accessibilities
                .Include(a => a.Accomodations)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Accessibility?> AddAsync(Accessibility accessibility)
        {
            _context.Accessibilities.Add(accessibility);
            await _context.SaveChangesAsync();
            return accessibility;
        }
    }
}
