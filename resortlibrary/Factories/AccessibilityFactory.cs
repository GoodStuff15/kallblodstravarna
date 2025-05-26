using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
{
    public class AccessibilityFactory : IAccessibilityFactory
    {
        public Accessibility CreateAcceessibility(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            
                throw new ArgumentNullException("Namn måste anges.", nameof(name));
                if(description == null)
                throw new ArgumentNullException("Beskrivning måste anges.", nameof(description));

            return new Accessibility
            {
                Name = name,
                Description = description
            };
            
        }
    }
}
