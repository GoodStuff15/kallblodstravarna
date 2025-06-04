using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using resortdtos;

namespace resortapi.Helpers;

public class BookingConfirmationDocument : IDocument
{
    private readonly BookingDetailsDto _booking;

    public BookingConfirmationDocument(BookingDetailsDto booking)
    {
        _booking = booking;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        if (_booking == null) throw new ArgumentNullException(nameof(_booking));

        container.Page(page =>
        {
            page.Margin(40);
            page.Size(PageSizes.A4);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Content()
                .Column(col =>
                {
                    col.Item().PaddingBottom(20).Text("Bokningsbekräftelse").FontSize(24).Bold();

                    col.Item().Text($"Datum: {DateTime.Now:d}");
                    col.Item().Text($"Bokningsnummer: {_booking.BookingId}");

                    if (_booking.Customer == null)
                    {
                        col.Item().Text("Kundinformation saknas").Italic();
                    }
                    else
                    {
                        col.Item().Text($"Kundnr: {_booking.Customer.Id}");
                        col.Item().Text($"Namn: {_booking.Customer.FirstName} {_booking.Customer.LastName}");
                        col.Item().Text($"E-post: {_booking.Customer.Email ?? "Saknas"}");
                        col.Item().Text($"Telefon: {_booking.Customer.PhoneNumber ?? "Saknas"}");
                        col.Item().Text($"Kundtyp: {_booking.Customer.Type ?? "Saknas"}");
                    }

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    col.Item().Text($"Incheckning: {_booking.CheckIn:yyyy-MM-dd}");
                    col.Item().Text($"Utcheckning: {_booking.CheckOut:yyyy-MM-dd}");
                    col.Item().Text($"Rum: {_booking.Accomodation?.Name ?? "Saknas"}");
                    col.Item().Text($"Rumstyp: {_booking.Accomodation?.AccomodationTypeName ?? "Saknas"}");

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    col.Item().Text("Gäster:").Bold();
                    if (_booking.Guests == null || !_booking.Guests.Any())
                    {
                        col.Item().Text("Inga gäster registrerade").Italic();
                    }
                    else
                    {
                        foreach (var guest in _booking.Guests)
                        {
                            col.Item().Text($"- {guest.FirstName} {guest.LastName}, {guest.Age} år");
                        }
                    }

                    if (_booking.AdditionalOptions?.Any() == true)
                    {
                        col.Item().PaddingTop(10).Text("Valda tillval:").Bold();
                        foreach (var opt in _booking.AdditionalOptions)
                        {
                            col.Item().Text($"- {opt.Name}: {opt.Price:C}");
                        }
                    }

                    col.Item().PaddingTop(20).Text("Tack för din bokning hos Kallblodstravarna Resort!")
                        .Italic();
                });
        });
    }
}
