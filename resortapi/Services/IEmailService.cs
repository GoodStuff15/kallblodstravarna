using resortdtos;

namespace resortapi.Services
{
    public interface IEmailService
    {
        Task SendBookingConfirmationEmailAsync(BookingDetailsDto booking, byte[] pdfBytes);
    }

}
