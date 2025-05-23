using restortlibrary.Factories.IFactories;
using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories
{
    public class BookingFactory : IBookingFactory
    {
        public Booking CreateBooking(DateTime checkIn, DateTime checkOut, Customer customer, Accomodation accomodation)
        {
            if (checkOut <= checkIn)
                throw new ArgumentException("Utcheckning måste vara efter incheckning.");
            if (customer == null)
                throw new ArgumentException("Kund måste anges.");
            if (accomodation == null)
                throw new ArgumentException("Boende måste anges.");
            if (checkIn < DateTime.Now.Date)
                throw new ArgumentException("Incheckning kan inte vara i då-tid.");

            return new Booking
            {
                CheckIn = checkIn,
                CheckOut = checkOut,
                Customer = customer,
                Accomodation = accomodation
            };
        }
    }
}
