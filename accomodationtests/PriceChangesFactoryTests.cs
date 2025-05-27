using resortlibrary.Builders;
using resortlibrary.Factories.IFactories;

namespace accomodationtests;

[TestClass]
public class PriceChangesFactoryTests
{
    private PriceChangesBuilder _factory;

    [TestInitialize]
    public void Setup()
    {
        _factory = new PriceChangesBuilder();
    }
    [TestMethod]
    public void CreatePriceChange_ValidData_ShouldReturnPriceChange()
    {
        float priceChange = 2.5f;
        string type = "Högsäsong";

        var result = _factory.CreatePriceChange(priceChange, type);
        Assert.IsNotNull(result);
        Assert.AreEqual(priceChange, result.PriceChange);
        Assert.AreEqual(type, result.Type);
        Assert.IsNotNull(result.Bookings);
        Assert.AreEqual(0, result.Bookings.Count);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreatePriceChange_InvalidType_ShouldThrow(string type)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreatePriceChange(2.5f, type));

        Assert.AreEqual("Typ måste anges.", ex.Message);
    }
}
