
using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class AccomodationBuilder
    {
        private Accomodation _accomodation;

        public AccomodationBuilder()
        {
            _accomodation = new Accomodation();
        }
        public AccomodationBuilder WithName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Namn måste anges.", nameof(name));

            _accomodation.Name = name;
            return this;
        }
        public AccomodationBuilder WithMaxOccupancy(int maxOccupancy)
        {
            if (maxOccupancy <= 0)
                throw new ArgumentException("Antalet gäster måste vara fler än 0.", nameof(maxOccupancy));

            _accomodation.MaxOccupancy = maxOccupancy;
            return this;
        }
        public AccomodationBuilder WithAccomodationType(AccomodationType accomodationType)
        {
            if (accomodationType == null)
                throw new ArgumentException("Boendetyp måste anges.", nameof(accomodationType));

            _accomodation.AccomodationType = accomodationType;
            return this;
        }
        public Accomodation Build()
        {
            if (_accomodation.Name == null)
                throw new InvalidOperationException("Namn har inte angetts.");
            if (_accomodation.AccomodationType == null)
                throw new InvalidOperationException("Boendetyp har inte angetts.");
            
            return _accomodation;
        }      
    }
}
