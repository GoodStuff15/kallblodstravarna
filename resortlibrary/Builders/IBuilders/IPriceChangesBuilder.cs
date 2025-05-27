using resortlibrary.Models;

namespace resortlibrary.Builders.IBuilders
{
    public interface IPriceChangesBuilder
    {
        PriceChangesBuilder AddPriceChange(float priceChange);

        PriceChangesBuilder AddType(string type);

        PriceChanges Build();
    }
}
