using resortlibrary.Models;
using restortlibrary.Factories.IFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories
{
    public class AdditionalOptionFactory : IAdditionalOptionFactory
    {
        public AdditionalOption CreateAdditionalOption(string name, string description, decimal price)
        {
            if(string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Namn måste anges.");
            if(price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Priset får inte vara lägre än 0.");

            return new AdditionalOption
            {
                Name = name,
                Description = description ?? string.Empty,
                Price = price,
                Bookings = new List<Booking>()
            };
        }
    }
}
