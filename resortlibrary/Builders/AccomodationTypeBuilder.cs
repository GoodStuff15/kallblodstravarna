using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class AccomodationTypeBuilder
    {
        private AccomodationType _accomodationType;

        public AccomodationTypeBuilder()
        {
            _accomodationType = new AccomodationType();
        }

        public AccomodationTypeBuilder AddName(string name)//Set name of accomodation type. Throw if null och whitespace
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required.");
            }

            _accomodationType.Name = name;
            return this;
        }

        public AccomodationTypeBuilder AddDescription(string description)//Set description
        {
            _accomodationType.Description = description;
            return this;
        }

        public AccomodationTypeBuilder AddBasePrice(decimal basePrice)//Set baseprice. Must be greater than 0
        {
            if (basePrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(basePrice), "Price must be greater than 0.");
            }

            _accomodationType.BasePrice = basePrice;
            return this;
        }

        public AccomodationTypeBuilder AddAccomodationList(ICollection<Accomodation> accomodations)//Set list of accomodations
        {
            _accomodationType.Accomodations = accomodations;
            return this;
        }

        public AccomodationType Build()//Build accomodation object
        {
            return _accomodationType;
        }
    }
}
