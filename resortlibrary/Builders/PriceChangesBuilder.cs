using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class PriceChangesBuilder
    {
        private PriceChanges _priceChange;

        public PriceChangesBuilder()
        {
            _priceChange = new PriceChanges();
        }

        public PriceChangesBuilder AddPriceChange(float priceChange)
        {
            if (float.IsNaN(priceChange) || float.IsInfinity(priceChange))
            {
                throw new ArgumentException("Prisändringen måste vara ett giltigt tal.");
            }
            _priceChange.PriceChange = priceChange;
            return this;
        }

        public PriceChangesBuilder AddType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Typ måste anges");
            }
            _priceChange.Type = type;
            return this;
        }

        public PriceChanges Build()
        {
            return _priceChange;
        }
    }
}
