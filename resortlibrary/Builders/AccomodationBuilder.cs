using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
{
    public class AccomodationFactory : IAccomodationBuilder
    {
        private string? _name;
        private int _maxOccupancy;
        private AccomodationType _accomodationType;

        public IAccomodationBuilder WithName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Namn måste anges.", nameof(name));

            _name = name;
            return this;
        }
        public IAccomodationBuilder WithMaxOccupancy(int maxOccupancy)
        {
            if (maxOccupancy <= 0)
                throw new ArgumentException("Antalet gäster måste vara fler än 0.", nameof(maxOccupancy);

            _maxOccupancy = maxOccupancy;
            return this;
        }
        public IAccomodationBuilder WithAccomodationType(AccomodationType accomodationType)
        {
            if (accomodationType == null)
                throw new ArgumentException("Boendetyp måste anges.", nameof(accomodationType);
            _accomodationType = accomodationType;
            return this;
        }
        public Accomodation Build()
        {
            if (_name == null)
                throw new InvalidOperationException("Namn har inte angetts.");
            if (_accomodationType == null)
                throw new InvalidOperationException("Boendetyp har inte angetts.");
            return new Accomodation
            {
                Name = _name,
                MaxOccupancy = _maxOccupancy,
                AccomodationType = _accomodationType,
                Bookings = new List<Booking>(),
                Accessibilities = new List<Accessibility>()
            };
        }      
    }
}
