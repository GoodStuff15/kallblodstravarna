using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortlibrary.Factories.IFactories
{
    public interface IBookingFactory
    {
        Booking CreateBooking(DateTime checkIn, DateTime checkOut, Customer customer, Accomodation accomodation);
    }
}
