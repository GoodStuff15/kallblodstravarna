using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using resortapi.Services;
using resortapi.Helpers;
using resortdtos;

public class PdfService : IPdfService
{
    public byte[] GenerateBookingPdf(BookingDetailsDto booking)
    {
        if (booking == null) throw new ArgumentNullException(nameof(booking));
        var document = new resortapi.Helpers.BookingConfirmationDocument(booking);
        return document.GeneratePdf();
    }
}
