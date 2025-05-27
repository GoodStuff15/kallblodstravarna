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
                throw new ArgumentException("Förnamn måste anges.", nameof(firstName));

            _guest.FirstName = firstName;
            return this;
        }

        public GuestBuilder AddLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Efternamn måste anges.", nameof(lastName));

            _guest.LastName = lastName;
            return this;
        }

        public GuestBuilder AddAge(int age)
        {
            _guest.Age = age;
            return this;
        }

        public Guest Build()
        {
            return _guest;
        }

    }
}
