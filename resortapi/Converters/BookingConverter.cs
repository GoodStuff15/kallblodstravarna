using resortdtos;
using resortlibrary.Builders;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class BookingConverter : IConverter<Booking, BookingDto>
    {

        public Booking FromDTOtoObject(BookingDto dto)
        {
            var guestConverter = new GuestConverter();

            var guestList = guestConverter.FromDTOtoObject_Collection(dto.Guests);

            var booking = new BookingBuilder().AddCheckIn(dto.CheckIn)
                                              .AddCheckOut(dto.CheckOut)
                                              .AddAccomodationId(dto.AccomodationId)
                                              .AddCustomerId(dto.CustomerId)
                                              .AddGuestList(guestList)
                                              .Build();

            return booking;
        }

        public ICollection<Booking> FromDTOtoObject_Collection(ICollection<BookingDto> collection)
        {
            throw new NotImplementedException();
        }

        public ICollection<BookingsOverviewDto> FromObjectCollection_ToOverviewCollection(ICollection<Booking> bookings)
        {
            var overview = new List<BookingsOverviewDto>();

            foreach(var b in bookings)
            {
                var dto = new BookingsOverviewDto()
                {
                    BookingId = b.Id,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    AccomodationId = b.AccomodationId,
                    CustomerId = b.CustomerId,
                    Active = b.Active
                };
                overview.Add(dto);
            }
            return overview;
        }

        public BookingDto FromObjecttoDTO(Booking obj)
        {
            var dto = new BookingDto();

            var dtoGuests = new List<GuestDto>();

            var dtoOptionIds = new List<int>();

            foreach (var g in obj.Guests)
            {
                var gdto = new GuestDto();
                gdto.Age = g.Age;
                gdto.FirstName = g.FirstName;
                gdto.LastName = g.LastName;
                dtoGuests.Add(gdto);
            }

            foreach (var o in obj.AdditionalOptions)
            {
                dtoOptionIds.Add(o.Id);
            }

            dto.CheckIn = obj.CheckIn;
            dto.CheckOut = obj.CheckOut;
            dto.Guests = dtoGuests;
            dto.AccomodationId = obj.AccomodationId;
            dto.CustomerId = obj.CustomerId;
            dto.AdditionalOptionIds = dtoOptionIds;

            return dto;
        }

        public ICollection<BookingDto> FromObjecttoDTO_Collection(ICollection<Booking> collection)
        {
            var guestConverter = new GuestConverter();
            var dtos = new List<BookingDto>();

            foreach(var b in collection)
            {
                var dto = new BookingDto()
                {
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    AccomodationId = b.AccomodationId,
                    CustomerId = b.CustomerId,
                    Guests = guestConverter.FromObjecttoDTO_Collection(b.Guests),
                    //AdditionalOptionIds = null ////// !!!!!
                    AdditionalOptionIds = b.AdditionalOptions.Select(o => o.Id).ToList()
                };

                dtos.Add(dto);
            }
            return dtos;
        }

        public Booking ModifyDtoToObject(ModifyBookingDto dto)
        {
            var guestConverter = new GuestConverter();

            var guestList = guestConverter.FromDTOtoObject_Collection(dto.Guests);

            var booking = new BookingBuilder()
                              .AddCheckIn(dto.CheckIn)
                              .AddCheckOut(dto.CheckOut)
                              .AddAccomodationId(dto.AccomodationId)
                              .AddGuestList(guestList)
                              .Build();

            return booking;
        }
        public Booking FromDTOtoObject(BookingDto dto, List<AdditionalOption> additionaloption)
        {
            var guestConverter = new GuestConverter();

            var guestList = guestConverter.FromDTOtoObject_Collection(dto.Guests);

            var booking = new BookingBuilder().AddCheckIn(dto.CheckIn)
                                              .AddCheckOut(dto.CheckOut)
                                              .AddAccomodationId(dto.AccomodationId)
                                              .AddCustomerId(dto.CustomerId)
                                              .AddGuestList(guestList)
                                              .AdditionalOptions(additionaloption)
                                              .Build();

            return booking;
        }
    }
}
