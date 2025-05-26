using Microsoft.IdentityModel.Tokens;
using restortlibrary.Factories;
using restortlibrary.Factories.IFactories;

namespace accomodationtests;

[TestClass]
public class GuestFactoryTests
{
    private IGuestFactory _factory;
    [TestInitialize]
    public void Setup()
    {
        _factory = new GuestFactory();
    }
    [TestMethod]
    public void CreateGuest_ValidData_ShouldReturnGuest()
    {
        var guest = _factory.CreateGuest("Johan", "Hansson");

        Assert.IsNotNull(guest);
        Assert.AreEqual("Johan", guest.FirstName);
        Assert.AreEqual("Hansson", guest.LastName);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateGuest_InvalidFirstName_ShouldThrow(string firstName)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateGuest(firstName, "Johan"));

        Assert.AreEqual("Förnamn måste anges.", ex.Message);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateGuest_InvalidLastName_ShouldThrow(string lastName)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateGuest(lastName, "Hansson"));

        Assert.AreEqual("Efternamn måste anges.", ex.Message);
    }
}
