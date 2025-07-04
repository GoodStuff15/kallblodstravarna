﻿using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly IBookingConverter _converter;
        private readonly IPdfService _pdfService;
        private readonly IEmailService _emailService;
        private readonly ICalculatorService _calculatorService;

        public BookingService(IBookingConverter converter, IBookingRepository repo, IPdfService pdfService, IEmailService emailService, ICalculatorService calculatorService)
        {
            _converter = converter;
            _repo = repo;
            _pdfService = pdfService;
            _emailService = emailService;
            _calculatorService = calculatorService;
        }

        public Booking ConvertToBooking(BookingDto booking)
        {
            return _converter.FromDTOtoObject(booking);
        }

        public BookingDto ConvertFromBooking(Booking booking)
        {
            return _converter.FromObjecttoDTO(booking);
        }

        public BookingsOverviewDto ConvertToOverview(Booking booking)
        {
            return _converter.FromObjectToOverviewDTO(booking);
        }

        public async Task<BookingDetailsDto> GetBooking(int id)
        {
            var booking = await _repo.GetAsync(id);
            if (ValidateBooking(booking))
            {
                return _converter.FromObjectToDetailedDTO(booking);
            }
            else
            {
                throw new Exception("Requested booking can't be validated");
            }
        }


        public async Task RemoveBooking(int id)
        {
            await _repo.DeleteAsync(await _repo.GetAsync(id));
        }

        public async Task<BookingDetailsDto> CreateBooking(BookingDto booking)
        {
            // Conversion
            var newBooking = ConvertToBooking(booking);

            // Validating and setting time of booking
            if (!ValidateBooking(SetTimeOfBooking(newBooking)))
            {
                throw new Exception("Error creating booking");
            }

            await _repo.CreateAsync(newBooking);
            var detailedDto = _converter.FromObjectToDetailedDTO(newBooking);

            try
            {
                var pdfBytes = _pdfService.GenerateBookingPdf(detailedDto);
                await _emailService.SendBookingConfirmationEmailAsync(detailedDto, pdfBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid e-postutskick: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }

            return detailedDto;
        }

        public async Task<bool> CancelBooking(int bookingId)
        {
            // Get from database
            var cancelThis = await _repo.GetAsync(bookingId);

            // Validation
            if (ValidateBooking(cancelThis))
            {
                // Adjust status and save to db
                cancelThis.Cancelled = true;
                cancelThis.Active = false;
                cancelThis.CancellationDate = DateTime.Now;

                await _repo.UpdateAsync(cancelThis);

                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<ICollection<BookingDetailsDto>> GetDetailedOverview()
        {
            var allBookings = await _repo.GetAllWithIncludesAsync();

            // Convert
            var dtos = new List<BookingDetailsDto>();

            foreach (var booking in allBookings)
            {
                dtos.Add(_converter.FromObjectToDetailedDTO(booking));
            }

            return dtos;

        }

        public async Task<BookingDto> ModifyBooking(ModifyBookingDto booking)
        {
            var modifyThis = await _repo.GetAsync(booking.BookingId);

            var updated = _converter.ModifyDtoToObject(booking);

            modifyThis.CheckIn = updated.CheckIn;
            modifyThis.CheckOut = updated.CheckOut;
            modifyThis.AccomodationId = updated.AccomodationId;
            modifyThis.Guests = updated.Guests;
            modifyThis.AdditionalOptions = updated.AdditionalOptions;

            int guestCount = modifyThis.Guests.Count;
            int numberOfNights = (modifyThis.CheckOut - modifyThis.CheckIn).Days;

            var priceRequest = new PriceRequestDto
            {
                AccomodationId = modifyThis.AccomodationId,
                GuestCount = guestCount,
                Duration = numberOfNights,
                AdditonalOptionIds = modifyThis.AdditionalOptions.Select(o => o.Id).ToList()
            };

            modifyThis.Cost = await _calculatorService.CalculateCurrentPrice(priceRequest);

            await _repo.UpdateAsync(modifyThis);

            return ConvertFromBooking(modifyThis);
        }


        public Booking SetTimeOfBooking(Booking booking)
        {
            booking.TimeOfBooking = DateTime.Now;

            return booking;
        }

        public async Task<ICollection<BookingsOverviewDto>> GetBookingsOverview()
        {
            // Db operation
            var objectCollection = await _repo.GetAllWithIncludesAsync();

            // Validation
            foreach (var booking in objectCollection)
            {
                if (!ValidateBooking(booking))
                {
                    throw new Exception("Error validating booking in collection");
                }
            }

            // Conversion
            var collection = _converter.FromObjectCollection_ToOverviewCollection
                             (objectCollection);

            return collection;

        }

        public async Task<ICollection<BookingsOverviewDto>> GetCustomerBookings(int id)
        {
            // Db operation
            var bookings = await _repo.GetByCustomerIdWithIncludesAsync(id);

            // Conversion
            return _converter.FromObjectCollection_ToOverviewCollection(bookings);

        }

        public async Task<ICollection<BookingsOverviewDto>> GetCustomerBookingsByIdAndEmail(int customerId, string email)
        {
            // Hämta bokningar där både customerId och e-post stämmer
            var bookings = await _repo.GetByCustomerIdAndEmailAsync(customerId, email);

            return _converter.FromObjectCollection_ToOverviewCollection(bookings);
        }


        public bool ValidateBooking(Booking booking)
        {
            if (booking.CheckOut <= booking.CheckIn)
            {
                throw new ArgumentException("Booking check out must be later date than check in");
            }
            if (booking.Guests.Count <= 0)
            {
                throw new ArgumentException("Booking must contain guests");
            }
            if (booking == null)
            {
                throw new ArgumentNullException("Booking is null");
            }
            // etc. 
            return true;
        }
    }
}
