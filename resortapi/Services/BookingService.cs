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

        public bool CreateBooking(BookingDto booking)
        {
            var newBooking = ConvertToBooking(booking);
            if(ValidateBooking(newBooking))
            {
                _repo.CreateAsync(newBooking);
                return true;
            }
            else
            {
                return false; 
            }
        }

        public bool CancelBooking(int bookingId)
        {
            var cancelThis = _repo.GetAsync(bookingId).Result;

            if(ValidateBooking(cancelThis))
            {
                cancelThis.Cancelled = true;
                cancelThis.Active = false;
                _repo.UpdateAsync(cancelThis);

                return true;
            }
            else
            {
                return false;
            }
        
        }

        public bool UpdateBooking(ModifyBookingDto booking)
        {
            var modifyThis = _repo.GetAsync(booking.BookingId).Result;

            var updated = _converter.ModifyDtoToObject(booking);

            // Updating here

            modifyThis.CheckIn = updated.CheckIn;
            modifyThis.CheckOut = updated.CheckOut;
            modifyThis.AccomodationId = updated.AccomodationId;
            modifyThis.Guests = updated.Guests;
            modifyThis.AdditionalOptions = updated.AdditionalOptions;

            _repo.UpdateAsync(modifyThis);

            return true;

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
                throw new ArgumentException("Booking check out must be later date than check in")
            }
            if(booking.Guests.Count <= 0)
            {
                throw new ArgumentException("Booking must contain guests");
            }
            // etc. 
            return true;
        }
    }
}
