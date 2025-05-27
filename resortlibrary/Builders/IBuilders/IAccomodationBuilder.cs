using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IAccomodationBuilder
    {
        IAccomodationBuilder WithName(string name);
        IAccomodationBuilder WithMaxOccupancy(int maxOccupancy);
        IAccomodationBuilder WithAccomodationType(AccomodationType accomodationType);

        Accomodation Build();

    }
}
