using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IPriceChanges
    {
        PriceChanges CreatePriceChange(float priceChange, string type);
    }
}
