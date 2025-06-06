using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class AccessibilityBuilder
    {
        private string? _name;
        private string? _description;
        private ICollection<Accomodation> _accomodations;

        public AccessibilityBuilder WithName(string name)//Set name of accessibility. Throw if null or whitespace
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required.");
            }

            _name = name;
            return this;
        }

        public AccessibilityBuilder WithDescription(string description)//Set description of accessibility. Throw if null or whitespace
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description is reguired.");
            }

            _description = description;
            return this;
        }

        public AccessibilityBuilder WithAccomodations(ICollection<Accomodation> accomodations)//Set list of accomodation. Throw if list is null or empty
        {
            if (accomodations == null || accomodations.Count == 0)
            {
                throw new ArgumentException("List with accomodation must be provided and can't be empty.", nameof(accomodations));
            }

            _accomodations = accomodations;
            return this;
        }

        public Accessibility Build()//Build and return object
        {
            return new Accessibility
            {
                Name = _name!,
                Description = _description!,
                Accomodations = _accomodations!
            };
        }
    }
}
