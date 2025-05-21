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

        public new async Task CreateAsync(Booking booking)
        {
            var currentCustomerBookings = from b in _context.Bookings
                                          where b.Customer.Id == booking.Customer.Id
                                          select b;

            if(currentCustomerBookings.Any())
            {
                var notAvailable = from c in currentCustomerBookings
                                   where c.Active == true
                                   select new { CheckIn = c.CheckIn, CheckOut = c.CheckOut };

                foreach (var dateRange in notAvailable) 
                {
                    if (booking.CheckIn < dateRange.CheckOut && dateRange.CheckIn < booking.CheckOut) 
                    {
                        throw new Exception("Customer currently has a booking with conflicting dates");
                    }
                    else
                    {
                        await _context.Set<Booking>().AddAsync(booking);
                    }
                }
            }                
        }
    }
}


// Controller => Translator (tolkar och skapar objekt i en factory) => Repository => databasen
// Och sen eventuellt tillbaka. 