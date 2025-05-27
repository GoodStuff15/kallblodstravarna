
using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class AccessibilityBuilder
    {
        private string? _name;
        private string? _description;

        public AccessibilityBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Namn måste anges.");
            }    
            _name = name;
            return this;
        }

        public AccessibilityBuilder WithDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Beskrivning måste anges.");
            }
            _description = description;
            return this;
        }

        public Accessibility Build()
        {
            return new Accessibility
            {
                Name = _name!,
                Description = _description!
            };
        }
    }
}
