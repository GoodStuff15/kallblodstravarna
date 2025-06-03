using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public interface IBookingService
    {
        public bool ValidateBooking(Booking booking);

        public Booking ConvertToBooking(BookingDto booking);

        public BookingDto ConvertFromBooking(Booking booking);

        public Task<bool> CreateBooking(BookingDto booking);

        public Task<bool> CancelBooking(int bookingId);  

        public ICollection<BookingsOverviewDto> GetBookingsOverview();

        public Task<BookingDto> ModifyBooking(ModifyBookingDto booking);
        
    }
}
