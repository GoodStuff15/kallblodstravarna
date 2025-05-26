using restortlibrary.Factories.IFactories;
using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories
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
