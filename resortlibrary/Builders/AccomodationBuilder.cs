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

        public AccomodationBuilder WithName(string? name)//Set name of accomodation. Throws if null och whitespace
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required");
            }

            _accomodation.Name = name;
            return this;
        }

        public AccomodationBuilder WithMaxOccupancy(int maxOccupancy)//Number of guests must be greater than 0
        {
            if (maxOccupancy <= 0)
            {
                throw new ArgumentException("Number of guests must be greater than 0.");
            }

            _accomodation.MaxOccupancy = maxOccupancy;
            return this;
        }

        public AccomodationBuilder WithAccomodationTypeId(int accomodationTypeId)//Set FK for accomodationtype. Must be greater than 0.
        {
            if (accomodationTypeId <= 0)
            {
                throw new ArgumentException("Accomodationtype-ID must be greater than 0.", nameof(accomodationTypeId));
            }

            _accomodation.AccomodationTypeId = accomodationTypeId;
            return this;
        }

        public AccomodationBuilder WithAccomodationType(AccomodationType accomodationType)//Set accomodationtype. Throw if null
        {
            if (accomodationType == null)
            {
                throw new ArgumentException("Accomodation type is required.");
            }

            _accomodation.AccomodationType = accomodationType;
            return this;
        }
        public AccomodationBuilder WithBookings(ICollection<Booking> bookings)//Set bookings. Filters out null if there is any.
        {
            if (bookings == null)
            {
                _accomodation.Bookings = new List<Booking>();
            }
            else
            {
                var filteredBookings = bookings.Where(b => b != null).ToList();

                _accomodation.Bookings = filteredBookings;
            }
            return this;
        }

        public AccomodationBuilder WithAccessibilities(ICollection<Accessibility> accessibilities)//Set accessibility option. Throws if null
        {
            if (accessibilities == null)
            {
                throw new ArgumentNullException(nameof(accessibilities), "Accessibility cant be null.");
            }

            _accomodation.Accessibilities = accessibilities;
            return this;
        }

        public Accomodation Build()//Build accomodation object
        {
            if (_accomodation.Name == null)
            {
                throw new InvalidOperationException("Name have not been entered.");
            }

            if (_accomodation.AccomodationType == null)
            {
                throw new InvalidOperationException("Accomodation type have not been entered");
            }

            return _accomodation;
        }
    }
}
