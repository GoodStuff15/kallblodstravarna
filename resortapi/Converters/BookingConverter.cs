using resortdtos;
using resortlibrary.Builders;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class BookingConverter : IConverter<Booking, BookingDto>
    {

        public Booking FromDTOtoObject(BookingDto dto)
        {
            var booking = new BookingBuilder().AddCheckIn(dto.CheckIn)
                                              .AddCheckOut(dto.CheckOut)
                                              .AddCost(dto.Cost)
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

            foreach (var g in obj.Guests)
            {
                var gdto = new GuestDto();
                gdto.Age = g.Age;
                gdto.FirstName = g.FirstName;
                gdto.LastName = g.LastName;
                dtoGuests.Add(gdto);
            }

            dto.CheckIn = obj.CheckIn;
            dto.CheckOut = obj.CheckOut;
            dto.Cost = obj.Cost;
            dto.Guests = dtoGuests;
            dto.AccomodationId = obj.AccomodationId;

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
                    Cost = b.Cost,
                    Guests = guestConverter.FromObjecttoDTO_Collection(b.Guests),
                    AdditionalOptionIds = null ////// !!!!!
                };

                dtos.Add(dto);
            }
            return dtos;
        }

        public Booking ModifyDtoToObject(ModifyBookingDto dto)
        {
            var booking = new Booking();

            var objGuests = new List<Guest>();

            foreach (var g in dto.Guests)
            {
                var gObj = new Guest();
                gObj.Age = g.Age;
                gObj.FirstName = g.FirstName;
                gObj.LastName = g.LastName;
                objGuests.Add(gObj);
            }

            booking.Id = dto.BookingId;
            booking.CheckIn = dto.CheckIn;
            booking.CheckOut = dto.CheckOut;
            booking.AccomodationId = dto.AccomodationId;
            //booking.AdditionalOptions = ??
            booking.Guests = objGuests;

            return booking;
        }
    }
}
