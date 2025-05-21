using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using restortlibrary.Data;
using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Repositories
{
    public class BookingRepo : AbstractRepo<Booking>
    {
        public BookingRepo(ResortContext context) : base(context)
        {
            _context = context;
        }

        public new async Task CreateAsync(Booking newBooking)
        {

            // Checking if accomodation is booked

            var accomodation = _context.Set<Accomodation>()
                              .Where(a => a.Id == newBooking.Accomodation.Id)
                              .FirstOrDefault();

            foreach(var b in accomodation.Bookings)

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

            if(newBooking.Guests.IsNullOrEmpty())
            {
                throw new Exception("Database error: No guests present in booking");
            }

            // Checking that number of guests are not bigger than accomodation max occupancy

            if(newBooking.Guests.Count > newBooking.Accomodation.MaxOccupancy)
            {
                throw new Exception("Database error: Number of guests in booking is greater than accomodation max capacity");
            }

             _context.Set<Booking>().Add(newBooking);
            
            await _context.SaveChangesAsync();
        }
    }
}

