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
        private readonly IRepository<Customer> _customerRepo;

        public BookingConverter(IRepository<Accomodation> repo, IConverter<Guest, GuestDto> converter, IRepository<AdditionalOption> options, IRepository<Customer> customerRepo)
        {
            _accomodationRepo = repo;
            _guestConverter = converter;
            _additionalOptions = options;
            _customerRepo = customerRepo;
        }

        public BookingDetailsDto FromObjectToDetailedDTO(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            return new BookingDetailsDto
            {
                BookingId = booking.Id,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                TimeOfBooking = booking.TimeOfBooking,
                Active = booking.Active,
                Cancelled = booking.Cancelled,
                CancellationDate = booking.CancellationDate,
                Cost = booking.Cost,
                AmountPaid = booking.AmountPaid,
                Customer = booking.Customer == null ? null : new CustomerDto
                {
                    Id = booking.Customer.Id,
                    FirstName = booking.Customer.FirstName,
                    LastName = booking.Customer.LastName,
                    Email = booking.Customer.Email,
                    PhoneNumber = booking.Customer.Phone,
                    Type = booking.Customer.Type
                },
                Accomodation = booking.Accomodation == null ? null : new AccomodationDto
                {
                    Id = booking.Accomodation.Id,
                    Name = booking.Accomodation.Name,
                    MaxOccupancy = booking.Accomodation.MaxOccupancy,
                    AccomodationTypeName = booking.Accomodation.AccomodationType?.Name,
                    Accessibilities = booking.Accomodation.Accessibilities?
                        .Select(a => a.Name)
                        .ToList() ?? new List<string>()
                },
                Guests = booking.Guests?
                    .Select(g => new GuestDto
                    {
                        FirstName = g.FirstName,
                        LastName = g.LastName,
                        Age = g.Age
                    }).ToList() ?? new List<GuestDto>(),
                AdditionalOptions = booking.AdditionalOptions?
                    .Select(o => new AdditionalOptionDto
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Description = o.Description,
                        Price = o.Price,
                        PerGuest = o.PerGuest,
                        PerNight = o.PerNight
                    }).ToList() ?? new List<AdditionalOptionDto>(),
                PriceChanges = booking.PriceChanges?
                    .Select(p => new PriceChangesDto
                    {
                        Id = p.Id,
                        PriceChange = p.PriceChange,
                        Type = p.Type
                    }).ToList() ?? new List<PriceChangesDto>()
            };
        }


        public Booking FromDTOtoObject(BookingDto dto)
        {

            var guestList = _guestConverter.FromDTOtoObject_Collection(dto.Guests);

            var accomodation = _accomodationRepo.GetAsync(dto.AccomodationId).Result;

            var optionsList = new List<AdditionalOption>();

            var customer = _customerRepo.GetAsync(dto.CustomerId).Result;

            foreach (var option in dto.AdditionalOptionIds)
            {
                optionsList.Add(_additionalOptions.GetAsync(option).Result);
            }

            Console.WriteLine($"DTO Cost: {dto.Cost}");

            var booking = new BookingBuilder().AddCheckIn(dto.CheckIn)
                                              .AddCheckOut(dto.CheckOut)
                                              .AddAccomodationId(dto.AccomodationId)
                                              .AddAccomodation(accomodation)
                                              .AddCustomerId(dto.CustomerId)
                                              .AddCustomer(customer)
                                              .AddGuestList(guestList)
                                              .AdditionalOptions(optionsList)
                                              .AddCost(dto.Cost)
                                              .Build();

            Console.WriteLine($"Booking Cost: {booking.Cost}");

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
                    CustomerFirstName = b.Customer?.FirstName,
                    CustomerLastName = b.Customer?.LastName,
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
                    AdditionalOptionIds = b.AdditionalOptions.Select(o => o.Id).ToList()

                };

                dtos.Add(dto);
            }
            return dtos;
        }

        public BookingsOverviewDto FromObjectToOverviewDTO(Booking booking)
        {
            return new BookingsOverviewDto()
            {
                BookingId = booking.Id,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                AccomodationId = booking.AccomodationId,
                CustomerId = booking.CustomerId,
                CustomerFirstName = booking.Customer.FirstName,
                CustomerLastName = booking.Customer.LastName,
                Active = booking.Active,
                Cost = booking.Cost
            };
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

        ICollection<BookingDto> IConverter<Booking, BookingDto>.FromObjectCollection_ToOverviewCollection(ICollection<Booking> objects)
        {
            throw new NotImplementedException();
        }
    }
}
