using resortlibrary.Models;
using resortlibrary.Builders.IBuilders;

namespace resortlibrary.Builders
{
    public class AccomodationTypeBuilder : IAccomodationTypeBuilder
    {
        private AccomodationType _accomodationType;

        public AccomodationTypeBuilder()
        {
            _accomodationType = new AccomodationType();
        }

        public AccomodationTypeBuilder AddName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Namn måste anges.");

            _accomodationType.Name = name;
            return this;
        }
        public AccomodationTypeBuilder AddDescription(string description)
        {
            _accomodationType.Description = description;
            return this;
        }

        public AccomodationTypeBuilder AddBasePrice(decimal basePrice)
        {
            if (basePrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(basePrice), "Pris måste vara högre än 0.");
          
            _accomodationType.BasePrice = basePrice;
            return this;
        }

        public AccomodationTypeBuilder AddAccomodationList(ICollection<Accomodation> accomodations)
        {
            _accomodationType.Accomodations = accomodations;
            return this;
        }

        public AccomodationType Build()
        {
            return _accomodationType;
        }
    }
}
