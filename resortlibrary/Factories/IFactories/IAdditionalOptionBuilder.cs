using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IAdditionalOptionBuilder
    {
        AdditionalOptionBuilder AddName(string name);
        AdditionalOptionBuilder AddDescription(string description);
        AdditionalOptionBuilder AddPrice(decimal price);
        AdditionalOption Build();

    }
}
