using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public interface IBookingConverter : IConverter<Booking, BookingDto>
    {
        public ICollection<BookingsOverviewDto> FromObjectCollection_ToOverviewCollection(ICollection<Booking> collection);

        public Booking ModifyDtoToObject(ModifyBookingDto dto);

        public BookingsOverviewDto FromObjectToOverviewDTO(Booking booking);
        BookingDetailsDto FromObjectToDetailedDTO(Booking booking);

    }
}
