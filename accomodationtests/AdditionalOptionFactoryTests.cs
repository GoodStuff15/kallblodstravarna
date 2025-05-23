using restortlibrary.Factories;
using restortlibrary.Factories.IFactories;

namespace accomodationtests;

[TestClass]
public class AdditionalOptionFactoryTests
{
    private IAdditionalOptionFactory _factory;

    [TestInitialize]
    public void Setup()
    {
        _factory = new AdditionalOptionFactory();
    }
    [TestMethod]
    public void CreateAdditionalOption_ValidData_ShouldReturnAddedOption()
    {
        var option = _factory.CreateAdditionalOption("Frukost", "Buffé", 100m);

        Assert.IsNotNull(option);
        Assert.AreEqual("Frukost", option.Name);
        Assert.AreEqual("Buffé", option.Description);
        Assert.AreEqual(100m, option.Price);
        Assert.IsNotNull(option.Bookings);
        Assert.AreEqual(0, option.Bookings.Count);
    }
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateAdditionalOption_InvalidName_ShouldThrow()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateAdditionalOption(name, "Beskrivning", 50m));
        Assert.AreEqual("Namn måste anges.", ex.Message);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow(-0.1)]
    [DataRow(-99.99)]
    public void CreateAdditionalOption_InvalidPrice_ShouldThrow(decimal price)
    {
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        _factory.CreateAdditionalOption("Frukost", "Beskrivning", (decimal)price));
        Assert.AreEqual("price", ex.ParamName);
    }
}
