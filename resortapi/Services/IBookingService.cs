using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public interface IBookingService
    {
        public bool ValidateBooking(Booking booking);

        public Booking ConvertToBooking(BookingDto booking);

        public bool CreateBooking(BookingDto booking);

        public ICollection<BookingsOverviewDto> GetBookingsOverview();
        
    }
}
