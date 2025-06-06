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

        public GuestBuilder AddFirstName(string firstName)//Set first name. Cant be null och whitespace
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name is required.");
            }

            _guest.FirstName = firstName;
            return this;
        }

        public GuestBuilder AddLastName(string lastName)//Set last name. Cant be null och whitespace
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name is required.");
            }

            _guest.LastName = lastName;
            return this;
        }

        public GuestBuilder AddAge(int age)//Set guests age. Must be greater than 0
        {
            if(age <= 0)
            {
                throw new ArgumentException("Age must be greater than 0.");
            }
            _guest.Age = age;
            return this;
        }

        public GuestBuilder WithBooking(Booking booking)//Associates guest with booking-object. Booking-object cant be null
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Booking must be entered.");
            }

            _guest.Booking = booking;
            _guest.BookingId = booking.Id;
            return this;
        }

        public GuestBuilder WithBookingId(int bookingId)//Set booking id. Must be greater than 0
        {
            if (bookingId <= 0)
            {
                throw new ArgumentException("Booking id must be greater than 0.", nameof(bookingId));
            }

            _guest.BookingId = bookingId;
            return this;
        }

        public Guest Build()//Build guest-object
        {
            _guest.IsChild = _guest.Age < 12;

            return _guest;
        }
    }
}
