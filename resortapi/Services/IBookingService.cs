using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public interface IBookingService
    {
        public bool ValidateBooking(Booking booking);

        public Booking ConvertToBooking(BookingDto booking);

        public BookingDto ConvertFromBooking(Booking booking);

        public BookingsOverviewDto ConvertToOverview(Booking booking);

        public Task<BookingDetailsDto> CreateBooking(BookingDto booking);

        public Task<BookingsOverviewDto> GetBooking(int id);

        public Task<bool> CancelBooking(int bookingId);

        public Task RemoveBooking(int bookingId);

        public Task<ICollection<BookingsOverviewDto>> GetBookingsOverview();

        public Task<BookingDto> ModifyBooking(ModifyBookingDto booking);
        
    }
}
