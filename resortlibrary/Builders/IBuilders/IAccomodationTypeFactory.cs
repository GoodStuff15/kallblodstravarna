using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IAccomodationTypeFactory
    {
        AccomodationType CreateAccomodationType(string name, decimal basePrice);
    }
}
