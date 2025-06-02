using resortapi.Repositories;
using resortdtos;
using resortlibrary.Builders;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class BookingConverter : IBookingConverter
    {

        private readonly IRepository<Accomodation> _accomodationRepo;
        private readonly IConverter<Guest, GuestDto> _guestConverter;
        private readonly IRepository<AdditionalOption> _additionalOptions;

        public BookingConverter(IRepository<Accomodation> repo, IConverter<Guest, GuestDto> converter, IRepository<AdditionalOption> options)
        {
            _accomodationRepo = repo;
            _guestConverter = converter;
            _additionalOptions = options;
        }

        public Booking FromDTOtoObject(BookingDto dto)
        {

            var guestList = _guestConverter.FromDTOtoObject_Collection(dto.Guests);

            var accomodation = _accomodationRepo.GetAsync(dto.AccomodationId).Result;

            var optionsList = new List<AdditionalOption>();

            foreach (var option in dto.AdditionalOptionIds)
            {
                optionsList.Add(_additionalOptions.GetAsync(option).Result);
            }

            var booking = new BookingBuilder().AddCheckIn(dto.CheckIn)
                                              .AddCheckOut(dto.CheckOut)
                                              .AddAccomodationId(dto.AccomodationId)
                                              .AddAccomodation(accomodation)
                                              .AddCustomerId(dto.CustomerId)
                                              .AddGuestList(guestList)
                                              .AdditionalOptions(optionsList)
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
            var dtos = new List<BookingDto>();

            foreach(var b in collection)
            {
                var dto = new BookingDto()
                {
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    AccomodationId = b.AccomodationId,
                    CustomerId = b.CustomerId,
                    Guests = _guestConverter.FromObjecttoDTO_Collection(b.Guests),
                    AdditionalOptionIds = null ////// !!!!!
                };

                dtos.Add(dto);
            }
            return dtos;
        }

        public Booking ModifyDtoToObject(ModifyBookingDto dto)
        {

            var accomodation = _accomodationRepo.GetAsync(dto.AccomodationId).Result;

            var optionsList = new List<AdditionalOption>();

            foreach(var option in dto.AdditionalOptionIds)
            {
                optionsList.Add(_additionalOptions.GetAsync(option).Result);
            }
            var guestList = _guestConverter.FromDTOtoObject_Collection(dto.Guests);

            var booking = new BookingBuilder()
                              .AddCheckIn(dto.CheckIn)
                              .AddCheckOut(dto.CheckOut)
                              .AddAccomodationId(dto.AccomodationId)
                              .AddGuestList(guestList)
                              .AddAccomodation(accomodation)
                              .AdditionalOptions(optionsList)
                              .Build();

            return booking;
        }


    }
}
