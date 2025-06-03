using Microsoft.EntityFrameworkCore;
using resortapi.Data;
using resortlibrary.Models;

namespace resortapi.Repositories
{
    public class BookingRepo : AbstractRepo<Booking>, IBookingRepository
    {
        public BookingRepo(ResortContext context) : base(context)
        {
            _context = context;
        }

        public new async Task CreateAsync(Booking newBooking)
        {

            // Checking if accomodation is booked

            var accomodation = await _context.Set<Accomodation>()
                         .Include(a => a.Bookings)
                         .FirstOrDefaultAsync(a => a.Id == newBooking.Accomodation.Id);

            foreach (var b in accomodation.Bookings)

            if (newBooking.CheckIn < b.CheckOut && b.CheckIn < newBooking.CheckOut)
            {
                throw new Exception("Database error: Room is already occupied during the selected dates");
            }

            // Checking that dates are not in the past

            if(newBooking.CheckIn < DateTime.Now || newBooking.CheckOut < DateTime.Now)
            {
                throw new Exception("Database error: Check in or check out dates have already passed");
            }

            // Checking that check out is after check in

            if(newBooking.CheckOut <= newBooking.CheckIn)
            {
                throw new Exception("Database error: Check out is on same or earlier date than check in");
            }

            // Checking that number of guests are bigger than zero

            //if(newBooking.Guests.IsNullOrEmpty())
            //{
            //    throw new Exception("Database error: No guests present in booking");
            //}

            // Checking that number of guests are not bigger than accomodation max occupancy

            if(newBooking.Guests.Count > newBooking.Accomodation.MaxOccupancy)
            {
                throw new Exception("Database error: Number of guests in booking is greater than accomodation max capacity");
            }

             _context.Set<Booking>().Add(newBooking);
            
            await _context.SaveChangesAsync();
        }

        public async Task CancelAsync(Booking booking)
        {
            //Checking if booking cancellation date has expired

            if(booking.CancellationDate < DateTime.Now)
            {
                throw new Exception("Database error: Cannot cancel booking, Last cancellation date has passed");
            }

            booking.Active = false;
            _context.Set<Booking>().Update(booking);
            await _context.SaveChangesAsync();
        }
    
        public override async Task<ICollection<Booking>> GetAllWithIncludesAsync()
        {
            return await _context.Set<Booking>()
                         .Include(g => g.Guests)
                         .Include(a => a.Accomodation)
                         .Include(o => o.AdditionalOptions)
                         .Include(c => c.Customer)
                         .ToListAsync();
        }

        public async Task<ICollection<Booking>> GetActiveBookingsAsync()
        {
            return await _context.Set<Booking>()
                         .Where(b => b.Active == true)
                         .ToListAsync();
        }

        public async Task<Booking?> GetAsync(int customerId)
        {
            return await _context.Set<Booking>()
                .Include(g => g.Guests)
                .Include(a => a.Accomodation)
                .Include(o => o.AdditionalOptions)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(b => b.Id == customerId);
        }
        public async Task<Booking> UpdateAsync(Booking booking)
        {
            _context.Set<Booking>().Update(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
        public async Task<Booking?> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Set<Booking>()
                .Include(b => b.Guests)
                .Include(b => b.Accomodation)
                    .ThenInclude(a => a.AccomodationType)
                .Include(b => b.Accomodation)
                    .ThenInclude(a => a.Accessibilities)
                .Include(b => b.AdditionalOptions)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<ICollection<Booking>> GetAllWithCustomerAsync()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .ToListAsync();
        }

    }

}