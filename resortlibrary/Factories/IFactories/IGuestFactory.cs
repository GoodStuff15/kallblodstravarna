using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IGuestBuilder
    {
        GuestBuilder AddFirstName(string firstName);
        GuestBuilder AddLastName(string lastName);
        GuestBuilder AddAge(int age);
        Guest Build();
    }
}
