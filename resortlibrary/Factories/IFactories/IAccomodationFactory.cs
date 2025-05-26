using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IAccomodationFactory
    {
        Accomodation CreateAccomodation(string? name, int maxOccupancy, AccomodationType accomodationType);
    }
}
