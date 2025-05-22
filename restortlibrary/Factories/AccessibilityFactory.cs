using restortlibrary.Factories.IFactories;
using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories
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
