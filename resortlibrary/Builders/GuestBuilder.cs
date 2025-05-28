using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class GuestBuilder
    {
        private Guest _guest;

        public GuestBuilder()
        {
            _guest = new Guest();
        }

        public GuestBuilder AddFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Förnamn måste anges.");
            }

            _guest.FirstName = firstName;
            return this;
        }

        public GuestBuilder AddLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Efternamn måste anges.");
            }

            _guest.LastName = lastName;
            return this;
        }

        public GuestBuilder AddAge(int age)
        {
            _guest.Age = age;
            return this;
        }

        public GuestBuilder WithBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Bokning måste anges.");
            }

            _guest.Booking = booking;
            _guest.BookingId = booking.Id;
            return this;
        }

        public GuestBuilder WithBookingId(int bookingId)
        {
            if (bookingId <= 0)
            {
                throw new ArgumentException("Boknings-ID måste vara större än 0.", nameof(bookingId));
            }

            _guest.BookingId = bookingId;
            return this;
        }

        public Guest Build()
        {
            if (_guest.Age < 12 && _guest.Age != null)
            {
                _guest.IsChild = true;
            }
            else
            {
                _guest.IsChild = false;
            }

            return _guest;
        }
    }
}
