using resortlibrary.Models;

namespace resortlibrary.Builders.IBuilders
{
    public interface IAccomodationBuilder
    {
        AccomodationBuilder WithName(string name);
        AccomodationBuilder WithMaxOccupancy(int maxOccupancy);
        AccomodationBuilder WithAccomodationType(AccomodationType accomodationType);

        Accomodation Build();
    }
}
