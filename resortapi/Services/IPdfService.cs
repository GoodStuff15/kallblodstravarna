using resortdtos;

namespace resortapi.Services
{
    public interface IPdfService
    {
        byte[] GenerateBookingPdf(BookingDetailsDto booking);
    }

}
