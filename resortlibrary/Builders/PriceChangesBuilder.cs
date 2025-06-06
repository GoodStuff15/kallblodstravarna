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

        public PriceChangesBuilder AddPriceChange(float priceChange)//Set price change amount. Must be a valid number.
        {
            if (float.IsNaN(priceChange) || float.IsInfinity(priceChange))
            {
                throw new ArgumentException("Price change must be a valid number.");
            }

            _priceChange.PriceChange = priceChange;
            return this;
        }

        public PriceChangesBuilder AddType(string type)//Set type of price change. Cant be null och whitespace
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Type must be entered.");
            }

            _priceChange.Type = type;
            return this;
        }

        public PriceChangesBuilder AddBookings(ICollection<Booking> bookings)//Set list of bookings connected to this price change
        {
            if (bookings == null || bookings.Count == 0)
            {
                throw new ArgumentException("Price change must be connected with at least one booking.");
            }

            _priceChange.Bookings = bookings;
            return this;
        }

        public PriceChanges Build()//Build price change-object
        {
            return _priceChange;
        }
    }
}
