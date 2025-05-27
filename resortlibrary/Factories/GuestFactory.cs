using resortlibrary.Factories.IFactories;
using resortlibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Factories
{
    public class GuestBuilder : IGuestFactory
    {
        private Guest _guest;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

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
