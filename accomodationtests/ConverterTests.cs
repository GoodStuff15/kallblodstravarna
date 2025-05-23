using resortdtos;
using restortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class ConverterTests
{
    [TestMethod]
    public void FromAccessibility_ToAccessibilityDTO ()
    {
        var dto = new AccessibilityDto();

        var obj = new Accessibility() { Id = 1, Name = "test",  Description = "Ett test", Accomodations = new List<Accomodation>()};
    
        dto.Description = obj.Description;
        dto.Name = obj.Name;
        
        Assert.AreEqual(obj.Name, dto.Name);
        Assert.AreEqual(obj.Description, dto.Description);
    }

    [TestMethod]
    public void FromAccessibilityDTO_ToAccessibility()
    {
        var obj = new Accessibility();

        var dto = new AccessibilityDto() { Name = "test", Description = "Ett test"};

        obj.Description = dto.Description;
        obj.Name = dto.Name;

        Assert.AreEqual(obj.Name, dto.Name);
        Assert.AreEqual(obj.Description, dto.Description);
    }
}
