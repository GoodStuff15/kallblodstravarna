﻿using Microsoft.EntityFrameworkCore;
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
            var availableRooms = _context.Set<Booking>()
                    .Where(b => b.CheckIn < end && start < b.CheckOut)
                    .Select(a => a.Accomodation);

            return await availableRooms.ToListAsync();
        }

        public async Task<ICollection<Accomodation>> GetAvailableByGuestNo(DateTime start, DateTime end, int noOfGuests)
        {
            var availableRooms = _context.Set<Accomodation>()
                        .Where(a => a.MaxOccupancy >= noOfGuests)
                        .Include(a => a.Bookings)
                        .SelectMany(
                            acc => acc.Bookings
                           .Where(date => !(start < date.CheckOut && date.CheckIn < end))
                           .Select(a => acc));
                        

            return await availableRooms.ToListAsync();

        }

        public override Task<ICollection<Accomodation>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
