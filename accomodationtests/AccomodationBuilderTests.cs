using resortlibrary.Builders;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class AccomodationBuilderTests
{
    private AccomodationBuilder _builder;

    [TestInitialize]
    public void Setup()
    {
        _builder = new AccomodationBuilder();
    }
    [TestMethod]
    public void CreateAccomodation_ValidInputs_ShouldReturnAccomodation()
    {
        var type = new AccomodationType { Id = 1, Name = "Strandvilla", BasePrice = 1000m };



        var accomodation = _builder.WithName("Strandvilla 1")
                           .WithMaxOccupancy(8)
                           .WithAccomodationType(type)
                           .Build();

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
        _builder.WithMaxOccupancy(0));

        Assert.AreEqual("Antalet gäster måste vara fler än 0.", ex.Message);
    }
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void CreateAccomodation_InvalidName_ShouldThrow(string name)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
            _builder.WithName(name)
            );
        Assert.AreEqual("Namn måste anges.", ex.Message);
    }
    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void CreateAccomodation_InvalidOccupancy_ShouldThrow(int maxOccupancy)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.WithMaxOccupancy(maxOccupancy)
        );
        Assert.AreEqual("Antalet gäster måste vara fler än 0.", ex.Message);
    }
    [TestMethod]
    public void CreateAccomodation_NullAccomodationType_ShouldThrow()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.WithAccomodationType(null)
        );
        Assert.AreEqual("Boendetyp måste anges.", ex.Message);
    }
    [TestMethod]
    public void Accomodation_WithNoBooking_ShouldReturnEmptyList()
    {
        var accomodation = new AccomodationBuilder()
             .WithName("TestBoende")
             .WithMaxOccupancy(4)
             .WithAccomodationType(new AccomodationType { Name = "Strandvilla", BasePrice = 1000m })
             .WithBookings(null)
             .Build();

        Assert.IsNotNull(accomodation.Bookings);
        Assert.AreEqual(0, accomodation.Bookings.Count);
    }
    [TestMethod]
    public void Accomodation_WithBookings_ShouldNotContainNull()
    {
        var validBooking = new Booking
        {
            CheckIn = new DateTime(2025, 7, 1),
            CheckOut = new DateTime(2025, 7, 5)
        };

        var accomodation = new AccomodationBuilder()
            .WithName("TestBoende")
            .WithMaxOccupancy(4)
            .WithAccomodationType(new AccomodationType { Name = "Strandvilla", BasePrice = 1000m })
            .WithBookings(new List<Booking> { validBooking, null })
            .Build();

        bool containsNull = accomodation.Bookings.Any(b => b == null);
        Assert.IsFalse(containsNull, "Boendet innehåller null-värden.");
    }
    [TestMethod]
    public void Accomodation_Bookings_ShouldContainMinOneGuest()
    {
        var accomodation = new AccomodationBuilder()
            .WithName("Strandvilla 1")
            .WithMaxOccupancy(4)
            .WithAccomodationType(new AccomodationType { Name = "Strandvilla", BasePrice = 1100m })
            .WithBookings(new List<Booking>
            {
                new Booking
                {
                    Guests = new List<Guest> { new Guest { FirstName = "Johan", LastName = "Hansson", Age = 40 } }
                }
            })
            .Build();
        int guestCount = accomodation.Bookings.SelectMany(b => b.Guests ?? new List<Guest>()).Count();
        Assert.IsTrue(guestCount >= 1, "Boendet måste ha minst en gäst.");
    }
}
