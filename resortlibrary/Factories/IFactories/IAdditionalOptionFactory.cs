using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IAdditionalOptionFactory
    {
        AdditionalOption CreateAdditionalOption(string name, string description, decimal price);
    }
}
