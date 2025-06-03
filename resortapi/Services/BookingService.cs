using Microsoft.Identity.Client;
using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repo;
        private readonly IBookingConverter _converter;

        public BookingService(IBookingConverter converter, IRepository<Booking> repo)
        {
            _converter = converter;
            _repo = repo;
        }

        public Booking ConvertToBooking(BookingDto booking)
        {
            return _converter.FromDTOtoObject(booking);
        }

        public BookingDto ConvertFromBooking(Booking booking)
        {
            return _converter.FromObjecttoDTO(booking);
        }

        public async Task<bool> CreateBooking(BookingDto booking)
        {
            // Conversion
            var newBooking = ConvertToBooking(booking);
            
            // Validating and setting time of booking

            if (ValidateBooking(SetTimeOfBooking(newBooking)))
            {
                await _repo.CreateAsync(newBooking);
                return true;
            }
            else
            {
                return false; 
            }
        }

        public async Task<bool> CancelBooking(int bookingId)
        {
            // Get from database
            var cancelThis = await _repo.GetAsync(bookingId);

            // Validation
            if(ValidateBooking(cancelThis))
            {
                // Adjust status and save to db
                cancelThis.Cancelled = true;
                cancelThis.Active = false;
                await _repo.UpdateAsync(cancelThis);

                return true;
            }
            else
            {
                return false;
            }
        
        }

        public async Task<BookingDto> UpdateBooking(ModifyBookingDto booking)
        {
            var modifyThis = _repo.GetAsync(booking.BookingId).Result;

            // Converting to access guests and additional options

            var updated = _converter.ModifyDtoToObject(booking);
            
            // Updating here

            modifyThis.CheckIn = updated.CheckIn;
            modifyThis.CheckOut = updated.CheckOut;
            modifyThis.AccomodationId = updated.AccomodationId;
            modifyThis.Guests = updated.Guests;
            modifyThis.AdditionalOptions = updated.AdditionalOptions;

            // Database action
            await _repo.UpdateAsync(modifyThis);

            return ConvertFromBooking(modifyThis);

        }
        public Booking SetTimeOfBooking(Booking booking)
        {
            booking.TimeOfBooking = DateTime.Now;

            return booking;
        }
        public ICollection<BookingsOverviewDto> GetBookingsOverview()
        {
            
            var collection = _converter.FromObjectCollection_ToOverviewCollection
                             (_repo.GetAllWithIncludesAsync().Result);

            // Validation??

            return collection;
         
            
        }

        public bool ValidateBooking(Booking booking)
        {
            if(booking.CheckOut <= booking.CheckIn)
            {
                throw new ArgumentException("Booking check out must be later date than check in");
            }
            if(booking.Guests.Count <= 0)
            {
                throw new ArgumentException("Booking must contain guests");
            }
            if(booking == null)
            {
                throw new ArgumentNullException("Booking is null");
            }
            // etc. 
            return true;
        }
    }
}
