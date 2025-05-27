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
}
