using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
{
    public class AccomodationFactory : IAccomodationBuilder
    {
        public Accomodation CreateAccomodation(string? name, int maxOccupancy, AccomodationType accomodationType)
        {
            if(string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Namn måste anges.");
            if (maxOccupancy <= 0)
                throw new ArgumentException("Antalet gäster måste vara fler än 0.");
            if (accomodationType == null)
                throw new ArgumentException("Boendetyp måste anges.");

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
