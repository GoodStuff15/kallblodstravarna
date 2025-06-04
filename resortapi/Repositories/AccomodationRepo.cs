using Microsoft.EntityFrameworkCore;
using resortapi.Data;
using resortlibrary.Models;

namespace resortapi.Repositories
{
    public class AccomodationRepo : AbstractRepo<Accomodation>
    {
        public AccomodationRepo(ResortContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Accomodation>> GetAvailableAsync(DateTime start, DateTime end)
        {
            var booked = _context.Set<Booking>()
                .Where(b => b.Active && !b.Cancelled && b.CheckIn < end && start < b.CheckOut)
                .Select(b => b.AccomodationId);

            var available = _context.Set<Accomodation>()
                .Where(a => !booked.Contains(a.Id))
                .Include(a => a.AccomodationType)
                .Include(a => a.Accessibilities);

            return await available.ToListAsync();
        }

        public async Task<ICollection<Accomodation>> GetAvailableByGuestNo(DateTime start, DateTime end, int noOfGuests)
        {
            // Hämta bokade boenden som krockar med datumintervallet
            var bookedIds = _context.Set<Booking>()
                .Where(b => b.Active && !b.Cancelled && b.CheckIn < end && start < b.CheckOut)
                .Select(b => b.AccomodationId);

            // Filtrera boenden som inte är bokade och som klarar antal gäster
            var available = _context.Set<Accomodation>()
                .Where(a => !bookedIds.Contains(a.Id) && a.MaxOccupancy >= noOfGuests)
                .Include(a => a.AccomodationType)
                .Include(a => a.Accessibilities);

            return await available.ToListAsync();
        }


        public override Task<ICollection<Accomodation>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<ICollection<Accomodation>> GetAllAsync()
        {
            return await _context.Accommodations
                .Include(a => a.AccomodationType)
                .Include(a => a.Accessibilities)
                .ToListAsync();
        }
        public async Task<Accomodation?> GetByIdAsync(int id)
        {
            return await _context.Accommodations
                .Include(a => a.AccomodationType)
                .Include(a => a.Accessibilities)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Accomodation?> AddAsync(Accomodation accomodation)
        {
            _context.Accommodations.Add(accomodation);
            await _context.SaveChangesAsync();

            return await _context.Accommodations
                .Include(a => a.AccomodationType)
                .Include(a => a.Accessibilities)
                .FirstOrDefaultAsync(a => a.Id == accomodation.Id);
        }
        public async Task<Accomodation?> UpdateAsync(Accomodation accomodation)
        {
            _context.Accommodations.Update(accomodation);
            await _context.SaveChangesAsync();
            return await _context.Accommodations
                .Include(a => a.AccomodationType)
                .Include(a => a.Accessibilities)
                .FirstOrDefaultAsync(a => a.Id == accomodation.Id);

        }
        public async Task DeleteAsync(Accomodation accomodation)
        {
            _context.Accommodations.Remove(accomodation);
            await _context.SaveChangesAsync();
        }
    }
}
