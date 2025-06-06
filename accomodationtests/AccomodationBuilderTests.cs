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
        var type = new AccomodationType { Id = 1, Name = "Beach villa", BasePrice = 1000m };



        var accomodation = _builder.WithName("Beach villa 1")
                           .WithMaxOccupancy(8)
                           .WithAccomodationType(type)
                           .Build();

        Assert.IsNotNull(accomodation);
        Assert.AreEqual("Beach villa 1", accomodation.Name);
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
        var type = new AccomodationType { Id = 1, Name = "Beach villa", BasePrice = 1000m };
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.WithMaxOccupancy(0));

        Assert.AreEqual("Number of guests must be greater than 0.", ex.Message);
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
        Assert.AreEqual("Name is required.", ex.Message);
    }
    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void CreateAccomodation_InvalidOccupancy_ShouldThrow(int maxOccupancy)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.WithMaxOccupancy(maxOccupancy)
        );
        Assert.AreEqual("Number of guests must be greater than 0.", ex.Message);
    }
    [TestMethod]
    public void CreateAccomodation_NullAccomodationType_ShouldThrow()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.WithAccomodationType(null)
        );
        Assert.AreEqual("Accomodation type is required", ex.Message);
    }
    [TestMethod]
    public void Accomodation_WithNoBooking_ShouldReturnEmptyList()
    {
        var accomodation = new AccomodationBuilder()
             .WithName("Test Accomodation")
             .WithMaxOccupancy(4)
             .WithAccomodationType(new AccomodationType { Name = "Beach villa", BasePrice = 1000m })
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
            .WithName("Test Accomodation")
            .WithMaxOccupancy(4)
            .WithAccomodationType(new AccomodationType { Name = "Beach villa", BasePrice = 1000m })
            .WithBookings(new List<Booking> { validBooking, null })
            .Build();

        bool containsNull = accomodation.Bookings.Any(b => b == null);
        Assert.IsFalse(containsNull, "Accomodation contains null values.");
    }
    [TestMethod]
    public void Accomodation_Bookings_ShouldContainMinOneGuest()
    {
        var accomodation = new AccomodationBuilder()
            .WithName("Beach villa 1")
            .WithMaxOccupancy(4)
            .WithAccomodationType(new AccomodationType { Name = "Beach villa", BasePrice = 1100m })
            .WithBookings(new List<Booking>
            {
                new Booking
                {
                    Guests = new List<Guest> { new Guest { FirstName = "Johan", LastName = "Hansson", Age = 40 } }
                }
            })
            .Build();
        int guestCount = accomodation.Bookings.SelectMany(b => b.Guests ?? new List<Guest>()).Count();
        Assert.IsTrue(guestCount >= 1, "Accomodation must have at least one occupant.");
    }
}
