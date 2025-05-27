using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
{
    public class AccessibilityFactory : IAccessibilityBuilder
    {
        private string? _name;
        private string? _description;

        public IAccessibilityBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Namn måste anges.");
            _name = name;
            return this;
        }

        public IAccessibilityBuilder WithDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description), "Beskrivning måste anges.");
                
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
