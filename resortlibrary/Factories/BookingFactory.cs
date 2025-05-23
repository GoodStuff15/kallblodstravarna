using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
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
