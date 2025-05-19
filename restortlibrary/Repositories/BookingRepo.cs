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

            var notAvailable = from c in currentCustomerBookings
                               where c.Active == true
                               select new { c.CheckIn, c.CheckOut };

            foreach (var dateRange in notAvailable) 
            {
                var start = dateRange.CheckIn.Ticks;
                var end = dateRange.CheckOut.Ticks;
                if (booking.CheckIn.Ticks < end && start < booking.CheckOut.Ticks) 
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
