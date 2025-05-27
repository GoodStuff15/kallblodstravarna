using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
{
    public class AdditionalOptionBuilder : IAdditionalOptionBuilder
    {
        private AdditionalOption _additionalOption;

        public AdditionalOptionBuilder AddName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Namn måste anges.");

            _additionalOption.Name = name;
            return this;
        }

        public AdditionalOptionBuilder AddDescription(string description)
        {
            _additionalOption.Description = description;
            return this;
        }

        public AdditionalOptionBuilder AddPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Priset får inte vara lägre än 0.");

            _additionalOption.Price = price;
            return this;
        }

        public AdditionalOption Build()
        {
            return _additionalOption;
        }
    }
}
