using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IPriceChangesBuilder
    {
        PriceChangesBuilder AddPriceChange(float priceChange);

        PriceChangesBuilder AddType(string type);

        PriceChanges Build();
    }
}
