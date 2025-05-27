using resortlibrary.Models;

namespace resortlibrary.Builders.IBuilders
{
    public interface IAccessibilityBuilder
    {
        AccessibilityBuilder WithName(string name);
        AccessibilityBuilder WithDescription(string description);
        Accessibility Build();
    }
}
