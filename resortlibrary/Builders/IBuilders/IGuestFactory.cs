using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IGuestFactory
    {
        Guest CreateGuest(string firstName, string lastName);
    }
}
