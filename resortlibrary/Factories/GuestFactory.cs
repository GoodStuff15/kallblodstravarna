using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
{
    public class GuestFactory : IGuestFactory
    {
        public Guest CreateGuest(string firstName, string lastName)
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Förnamn måste anges.", nameof(firstName));
            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Efternamn måste anges.", nameof(lastName));

            return new Guest
            {
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}
