using restortlibrary.Factories;
using restortlibrary.Factories.IFactories;
using restortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class AccomodationFactoryTests
{
    private IAccomodationFactory _factory;

    [TestInitialize]
    public void Setup()
    {
        _factory = new AccomodationFactory();
    }
    [TestMethod]
    public void CreateAccomodation_MaxOccupancyZero_ShouldThrow()
    {
        var type = new AccomodationType { Id = 1, Name = "Strandvilla", BasePrice = 1000m };
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateAccomodation("Strandvilla 1", 0, type)
        );

        Assert.AreEqual("Antalet gäster måste vara fler än 0.", ex.Message);
    }
    [TestMethod]
    public void CreateAccomodation_ValidInputs_ShouldReturnAccomodation()
    {
        var type = new AccomodationType { Id = 1, Name = "Strandvilla", BasePrice = 1000m };

        var accomodation = _factory.CreateAccomodation("Strandvilla 1", 8, type);

        Assert.IsNotNull(accomodation);
        Assert.AreEqual("Strandvilla 1", accomodation.Name);
        Assert.AreEqual(8, accomodation.MaxOccupancy);
        Assert.AreEqual(type, accomodation.AccomodationType);
        Assert.IsNotNull(accomodation.Bookings);
        Assert.AreEqual(0, accomodation.Bookings.Count);
        Assert.IsNotNull(accomodation.Accessibilities);
        Assert.AreEqual(0, accomodation.Accessibilities.Count);
    }
    
    
}
