using resortlibrary.Builders;

namespace accomodationtests;

[TestClass]
public class AccessibilityBuilderTests
{
    private AccessibilityBuilder _builder;

    [TestInitialize]
    public void Setup()
    {
        _builder = new AccessibilityBuilder();
    }
    [TestMethod]
    public void CreateAccessibility_ValidData_ShouldReturnAccessibility()
    {
        string name = "Handikappsanpassat rum";
        string description = "Rullstolsramp";

        var accessibility = _builder.WithName(name)
            .WithDescription(description)
            .Build();

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
        _builder.WithName(invalidName));

        Assert.AreEqual("Name is required.", ex.Message);
    }

    [TestMethod]
    public void CreateAccessibility_NullDescription_ShouldThrow()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.WithDescription(null));

        Assert.AreEqual("Description is reguired.", ex.Message);
    }
    
}
