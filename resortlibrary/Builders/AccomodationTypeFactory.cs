using resortlibrary.Models;
using resortlibrary.Factories.IFactories;

namespace resortlibrary.Factories
{
    public class AccomodationTypeFactory : IAccomodationTypeFactory
    {
        public AccomodationType CreateAccomodationType(string name, decimal basePrice)
        {
            if(string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Namn måste anges.");
            if (basePrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(basePrice),"Pris måste vara högre än 0.");

            return new AccomodationType
            {
                Name = name,
                BasePrice = basePrice
            };
        }
    }
}
