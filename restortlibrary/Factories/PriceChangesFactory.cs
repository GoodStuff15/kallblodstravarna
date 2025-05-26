using restortlibrary.Factories.IFactories;
using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories
{
    public class PriceChangesFactory : IPriceChanges
    {
        public PriceChanges CreatePriceChange(float priceChange, string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Typ måste anges");

            return new PriceChanges
            {
                PriceChange = priceChange,
                Type = type
            };
        }

        
    }
}
