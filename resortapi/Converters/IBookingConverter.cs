using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public interface IBookingConverter : IConverter<Booking, BookingDto>
    {
        public ICollection<BookingsOverviewDto> FromObjectCollection_ToOverviewCollection(ICollection<Booking> collection);

        public Booking ModifyDtoToObject(ModifyBookingDto dto);

        BookingDetailsDto FromObjectToDetailedDTO(Booking booking);

    }
}
