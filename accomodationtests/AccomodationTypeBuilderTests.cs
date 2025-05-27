using resortlibrary.Builders;
using resortlibrary.Builders.IBuilders;

namespace accomodationtests;

[TestClass]
public class AccomodationTypeBuilderTests
{
    private IAccomodationTypeBuilder _builder;


    [TestInitialize]
    public void Setup()
    {
        _builder = new AccomodationTypeBuilder();
    }
    [TestMethod]
    public void CreateAccomodationType_ValidData_SouldReturnAccomodationType()
    {
        var type = _builder.CreateAccomodationType("Bungalow", 700m);

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
        _builder.CreateAccomodationType(name, 700m));
        Assert.AreEqual("Namn måste anges.", ex.Message);
    }
    [DataTestMethod]
    [DataRow(0.0)]
    [DataRow(-10.0)]
    public void CreateAccomodationType_InvalidPrice_ShouldThrow(double basePriceInput)
    {
        decimal basePrice = (decimal)basePriceInput;
        //var ex = Assert.ThrowsException<ArgumentException>(() =>
        //_builder.CreateAccomodationType("Bungalow", basePrice));
        //Assert.AreEqual("Pris måste vara högre än 0.", ex.Message);
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        _builder.CreateAccomodationType("Bungalow", basePrice));
        Assert.AreEqual("basePrice", ex.ParamName);
    }
}
