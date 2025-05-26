using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IBookingFactory
    {
        Booking CreateBooking(DateTime checkIn, DateTime checkOut, Customer customer, Accomodation accomodation);
    }
}
