using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IAccomodationTypeBuilder
    {
        public AccomodationTypeBuilder AddName(string name);
        public AccomodationTypeBuilder AddDescription(string description);
        public AccomodationTypeBuilder AddBasePrice(decimal basePrice);

        public AccomodationTypeBuilder AddAccomodationList(ICollection<Accomodation> accomodations);

        public AccomodationType Build();
    }
}
