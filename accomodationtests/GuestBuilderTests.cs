using resortlibrary.Builders;

namespace accomodationtests;

[TestClass]
public class GuestBuilderTests
{
    private GuestBuilder _builder;
    [TestInitialize]
    public void Setup()
    {
        _builder = new GuestBuilder();
    }
    [TestMethod]
    public void CreateGuest_ValidData_ShouldReturnGuest()
    {
        var guest = _builder.AddFirstName("Johan")
                            .AddLastName("Hansson")
                            .Build(); 

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
        _builder.AddFirstName(firstName));

        Assert.AreEqual("Förnamn måste anges.", ex.Message);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateGuest_InvalidLastName_ShouldThrow(string lastName)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.AddLastName(lastName));

        Assert.AreEqual("Efternamn måste anges.", ex.Message);
    }
    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void CreateGuest_InvalidAge_ShouldThrow(int invalidAge)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.AddAge(invalidAge));

        Assert.AreEqual("Ålder måste vara högre än 0", ex.Message);
    }
    [TestMethod]
    public void CreateGuest_NullBooking_ShouldThrow()
    {
        var ex = Assert.ThrowsException<ArgumentNullException>(() =>
        _builder.WithBooking(null));
        Assert.AreEqual("Bokning måste anges. (Parameter 'booking')", ex.Message);
    }
}
