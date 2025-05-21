using restortlibrary.Factories;
using restortlibrary.Factories.IFactories;

namespace accomodationtests;

[TestClass]
public class AccomodationTypeFactoryTests
{
    private IAccomodationTypeFactory _factory;


    [TestInitialize]
    public void Setup()
    {
        _factory = new AccomodationTypeFactory();
    }
    [TestMethod]
    public void CreateAccomodationType_ValidData_SouldReturnAccomodationType()
    {
        var type = _factory.CreateAccomodationType("Bungalow", 700m);

        Assert.IsNotNull(type);
        Assert.AreEqual("Bungalow", type.Name);
        Assert.AreEqual(700m, type.BasePrice);
    }
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateAccomodationType_InvalidName_ShouldThrow(string name)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateAccomodationType(name, 700m));
        Assert.AreEqual("Namn måste anges.", ex.Message);
    }
    [DataTestMethod]
    [DataRow(0.0)]
    [DataRow(-10.0)]
    public void CreateAccomodationType_InvalidPrice_ShouldThrow(double basePriceInput)
    {
        decimal basePrice = (decimal)basePriceInput;
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        _factory.CreateAccomodationType("Bungalow", basePrice));
        Assert.AreEqual("basePrice", ex.ParamName);
    }
}
