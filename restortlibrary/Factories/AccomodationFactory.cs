using restortlibrary.Factories.IFactories;
using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories
{
    public class AccomodationFactory : IAccomodationFactory
    {
        public Accomodation CreateAccomodation(string? name, int maxOccupancy, AccomodationType accomodationType)
        {
            if (maxOccupancy <= 0)
                throw new ArgumentException("Antalet gäster måste vara fler än 0.");

            return new Accomodation
            {
                Name = name,
                MaxOccupancy = maxOccupancy,
                AccomodationType = accomodationType,
                Bookings = new List<Booking>(),
                Accessibilities = new List<Accessibility>()
            };
        }
    }
}
