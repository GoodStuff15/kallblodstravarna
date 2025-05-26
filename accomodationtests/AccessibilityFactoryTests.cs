using resortlibrary.Factories;

namespace accomodationtests;

[TestClass]
public class AccessibilityFactoryTests
{
    private AccessibilityFactory _factory;

    [TestInitialize]
    public void Setup()
    {
        _factory = new AccessibilityFactory();
    }
    [TestMethod]
    public void CreateAccessibility_ValidData_ShouldReturnAccessibility()
    {
        string name = "Handikappsanpassat rum";
        string description = "Rullstolsramp";

        var accessibility = _factory.CreateAcceessibility(name, description);

        Assert.IsNotNull(accessibility);
        Assert.AreEqual(name, accessibility.Name);
        Assert.AreEqual(description, accessibility.Description);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateAccessibility_InvalidName_ShouldThrow(string invalidName)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateAcceessibility(invalidName, "Beskrivning"));

        Assert.AreEqual("Namn måste anges.", ex.Message);
    }

    [TestMethod]
    public void CreateAccessibility_NullDescription_ShouldThrow()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateAcceessibility("Namn,", null));

        Assert.AreEqual("Beskrivning måste anges.", ex.Message);
    }
    
}
