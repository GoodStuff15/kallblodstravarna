using resortlibrary.Builders;
using resortlibrary.Builders.IBuilders;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class AccomodationBuilderTests
{
    private IAccomodationBuilder _builder;

    [TestInitialize]
    public void Setup()
    {
        _builder = new AccomodationBuilder();
    }
    [TestMethod]
    public void CreateAccomodation_ValidInputs_ShouldReturnAccomodation()
    {
        var type = new AccomodationType { Id = 1, Name = "Strandvilla", BasePrice = 1000m };

        var accomodation = _builder.CreateAccomodation("Strandvilla 1", 8, type);

        Assert.IsNotNull(accomodation);
        Assert.AreEqual("Strandvilla 1", accomodation.Name);
        Assert.AreEqual(8, accomodation.MaxOccupancy);
        Assert.AreEqual(type, accomodation.AccomodationType);
        Assert.IsNotNull(accomodation.Bookings);
        Assert.AreEqual(0, accomodation.Bookings.Count);
        Assert.IsNotNull(accomodation.Accessibilities);
        Assert.AreEqual(0, accomodation.Accessibilities.Count);
    }
    [TestMethod]
    
    public void CreateAccomodation_MaxOccupancyZero_ShouldThrow()
    {
        var type = new AccomodationType { Id = 1, Name = "Strandvilla", BasePrice = 1000m };
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.CreateAccomodation("Strandvilla 1", 0, type)
        );

        Assert.AreEqual("Antalet gäster måste vara fler än 0.", ex.Message);
    }
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateAccomodation_InvalidName_ShouldThrow(string name)
    {
        var type = new AccomodationType { Id = 1, Name = "Strandvilla", BasePrice = 1000m };
        var ex = Assert.ThrowsException<ArgumentException>(() =>
            _builder.CreateAccomodation(name, 8, type)
            );
        Assert.AreEqual("Namn måste anges.", ex.Message);
    }
    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void CreateAccomodation_InvalidOccupancy_ShouldThrow(int maxOccupancy)
    {
        var type = new AccomodationType { Id = 1, Name = "Strandvilla", BasePrice = 1000m };
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.CreateAccomodation("Strandvilla 1", maxOccupancy, type)
        );
        Assert.AreEqual("Antalet gäster måste vara fler än 0.", ex.Message);
    }
    [TestMethod]
    public void CreateAccomodation_NullAccomodationType_ShouldThrow()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.CreateAccomodation("Strandvilla 1", 8, null)
        );
        Assert.AreEqual("Boendetyp måste anges.", ex.Message);
    }
    
    
    
}
