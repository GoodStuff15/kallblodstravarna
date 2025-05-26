using resortlibrary.Factories;
using resortlibrary.Factories.IFactories;

namespace accomodationtests;

[TestClass]
public class PriceChangesFactoryTests
{
    private PriceChangesFactory _factory;

    [TestInitialize]
    public void Setup()
    {
        _factory = new PriceChangesFactory();
    }
    [TestMethod]
    public void CreatePriceChange_ValidData_ShouldReturnPriceChange()
    {
        float priceChange = 2.5f;
        string type = "H�gs�song";

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

        Assert.AreEqual("Typ m�ste anges.", ex.Message);
    }
}
