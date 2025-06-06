using resortlibrary.Builders;

namespace accomodationtests;

[TestClass]
public class AdditionalOptionBuilderTests
{
    private AdditionalOptionBuilder _builder;

    [TestInitialize]
    public void Setup()
    {
        _builder = new AdditionalOptionBuilder();
    }
    [TestMethod]
    public void CreateAdditionalOption_ValidData_ShouldReturnAddedOption()
    {
        var option = _builder.AddName("Breakfast")
                    .AddDescription("Buffet")
                    .AddPrice(100m)
                    .Build();

        Assert.IsNotNull(option);
        Assert.AreEqual("Breakfast", option.Name);
        Assert.AreEqual("Buffet", option.Description);
        Assert.AreEqual(100m, option.Price);
        Assert.IsNotNull(option.Bookings);
        Assert.AreEqual(0, option.Bookings.Count);
    }
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateAdditionalOption_InvalidName_ShouldThrow(string name)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.AddName(name));
        Assert.AreEqual("Name is required.", ex.Message);
    }

    [DataTestMethod]
    [DataRow(-0.1)]
    [DataRow(-99.99)]
    public void CreateAdditionalOption_InvalidPrice_ShouldThrow(double inputPrice)
    {
        decimal price = (decimal)inputPrice;
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        _builder.AddPrice(price));
        Assert.AreEqual("price", ex.ParamName);
    }
}
