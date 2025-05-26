using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
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
