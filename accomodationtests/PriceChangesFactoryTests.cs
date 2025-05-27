using resortlibrary.Builders;
using resortlibrary.Builders.IBuilders;

namespace accomodationtests;

[TestClass]
public class PriceChangesFactoryTests
{
    private IPriceChangesBuilder _builder;

    [TestInitialize]
    public void Setup()
    { 
        _builder = new PriceChangesBuilder();
    }
    [TestMethod]
    public void CreatePriceChange_ValidData_ShouldReturnPriceChange()
    {
        float priceChange = 2.5f;

        var result = _builder.AddPriceChange(priceChange)
                     .Build();
        Assert.IsNotNull(result);
        Assert.AreEqual(priceChange, result.PriceChange);
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
        _builder.AddType(null));

        Assert.AreEqual("Typ måste anges.", ex.Message);
    }
}
