using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IAccessibilityBuilder
    {
        IAccessibilityBuilder WithName(string name);
        IAccessibilityBuilder WithDescription(string description);
        Accessibility Build();
    }
}
