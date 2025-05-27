using resortlibrary.Builders;
using resortlibrary.Builders.IBuilders;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class BookingBuilderTests
{
    private IBookingBuilder _builder;
    [TestInitialize]
    public void Setup()
    {
        _builder = new BookingBuilder();
    }
    [TestMethod]
    public void CreateBooking_ValidInputs_ShouldReturnBooking()
    {
        var customer = new Customer { Id = 1, FirstName = "Testperson" };
        var accomodation = new Accomodation { Id = 1, Name = "Rum" };
        var checkIn = new DateTime(2025, 7, 1);
        var checkOut = new DateTime(2025, 7, 5);

        var booking = 
                      

        Assert.IsNotNull(booking);
        Assert.AreEqual(customer, booking.Customer);
        Assert.AreEqual(accomodation, booking.Accomodation);
        Assert.AreEqual(checkIn, booking.CheckIn);
        Assert.AreEqual(checkOut, booking.CheckOut);
    }
    [TestMethod]
    public void CreateBooking_CheckOutBeforeCheckIn_ShouldThrow()
    {
        var customer = new Customer();
        var accomodation = new Accomodation();
        var checkIn = new DateTime(2025, 7, 5);
        var checkOut = new DateTime(2025, 7, 1);

        var ex = Assert.ThrowsException<ArgumentException>(() =>
                       _builder
                      .AddCustomer(customer)
                      .AddAccomodation(accomodation)
                      .AddCheckIn(checkIn)
                      .AddCheckOut(checkOut)
                      .Build());

        Assert.AreEqual("Utcheckning måste vara efter incheckning.", ex.Message);
    }
    [TestMethod]
    public void CreateBooking_NullCustomer_ShouldThrow()
    {
        var accomodation = new Accomodation();
        var checkIn = new DateTime(2025, 7, 1);
        var checkOut = new DateTime(2025, 7, 5);

        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.AddCustomer(null));
        Assert.AreEqual("Kund måste anges.", ex.Message);
    }
    [TestMethod]
    public void CreateBooking_NullAccomodation_ShouldThrow()
    {
        var customer = new Customer();
        Accomodation accomodation = null;
        var checkIn = new DateTime(2025, 7, 1);
        var checkOut = new DateTime(2025, 7, 5);

        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.AddAccomodation(accomodation));
        Assert.AreEqual("Boende måste anges.", ex.Message);
    }
    [TestMethod]
    public void CreateBooking_CheckInSameAsCheckOut_ShouldThrow()
    {
        var customer = new Customer();
        var accomodation = new Accomodation();
        var checkInOut = new DateTime(2025, 7, 1);

        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder
                      .AddCustomer(customer)
                      .AddAccomodation(accomodation)
                      .AddCheckIn(checkInOut)
                      .AddCheckOut(checkInOut)
                      .Build());
        Assert.AreEqual("Utcheckning måste vara efter incheckning.", ex.Message);
    }
    [TestMethod]
    public void CreateBooking_CheckInPastDate_ShouldThrow()
    {

        var checkIn = DateTime.Now.AddDays(-1);

        var ex = Assert.ThrowsException<ArgumentException> (() =>
        _builder.AddCheckIn(checkIn));

        Assert.AreEqual("Incheckning kan inte vara i då-tid.", ex.Message);
    }
    
}
