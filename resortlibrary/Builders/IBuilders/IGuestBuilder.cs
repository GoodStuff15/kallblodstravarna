using resortlibrary.Models;

namespace resortlibrary.Builders.IBuilders
{
    public interface IGuestBuilder
    {
        GuestBuilder AddFirstName(string firstName);
        GuestBuilder AddLastName(string lastName);
        GuestBuilder AddAge(int age);
        Guest Build();
    }
}
