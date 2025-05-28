using resortlibrary.Builders;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class BookingBuilderTests
{
    private BookingBuilder _builder;
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

        var booking = _builder.AddAccomodation(accomodation)
                        .AddCheckIn(checkIn)
                        .AddCheckOut(checkOut)
                        .AddCustomer(customer)
                        .Build();
                      

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
    [TestMethod]
    public void CreateBooking_WithoutTimeOfBooking_ShouldBeCurrentTime()
    {
        var customer = new Customer {Id = 0};
        var accomodation = new Accomodation { Id = 1};
        var checkIn = DateTime.Now.AddDays(1);
        var checkOut = DateTime.Now.AddDays(3);
        var guest = new Guest {Id = 1};

        var beforeBuild = DateTime.Now;

        var booking = _builder
                        .AddCustomer(customer)
                        .AddAccomodation(accomodation)
                        .AddCheckIn(checkIn)
                        .AddCheckOut(checkOut)
                        .AddGuestList(new List<Guest> { guest })
                        .AddTimeOfBooking(DateTime.Now)
                        .Build();

        var afterBuild = DateTime.Now;
        Assert.IsTrue(booking.TimeOfBooking >= beforeBuild && booking.TimeOfBooking <= afterBuild);
    }
    [TestMethod]
    public void CreateBooking_WithOutAmountPaid_ShouldBeZero()
    {
        var booking = _builder
            .AddCustomer(new Customer { Id = 1 })
            .AddAccomodation(new Accomodation { Id = 1 })
            .AddCheckIn(DateTime.Now.AddDays(1))
            .AddCheckOut(DateTime.Now.AddDays(3))
            .AddGuestList(new List<Guest> { new Guest { Id = 1 } })
            .Build();

        Assert.AreEqual(0, booking.AmountPaid);
    }
    [TestMethod]
    public void CreateBooking_WithOutActiveBooking_ShouldBeTrue()
    {
        var booking = _builder
            .AddCustomer(new Customer { Id = 1 })
            .AddAccomodation(new Accomodation { Id = 1 })
            .AddCheckIn(DateTime.Now.AddDays(1))
            .AddCheckOut(DateTime.Now.AddDays(3))
            .AddGuestList(new List<Guest> { new Guest { Id = 1 } })
            .Build();

        Assert.IsTrue(booking.Active);
    }
    [TestMethod]
    public void CreateBooking_WithOutCancelationDate_ShouldBeNull()
    {
        var booking = _builder
            .AddCustomer(new Customer { Id = 1 })
            .AddAccomodation(new Accomodation { Id = 1 })
            .AddCheckIn(DateTime.Now.AddDays(1))
            .AddCheckOut(DateTime.Now.AddDays(3))
            .AddGuestList(new List<Guest> { new Guest { Id = 1 } })
            .Build();

        Assert.IsNull(booking.CancellationDate);
    }
    [TestMethod]
    public void CreateBooking_WithOutGuest_ShouldThrow()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder
            .AddCustomer(new Customer { Id = 1 })
            .AddAccomodation(new Accomodation { Id = 1 })
            .AddCheckIn(DateTime.Now.AddDays(1))
            .AddCheckOut(DateTime.Now.AddDays(3))
            .AddGuestList(new List<Guest>())
            .Build());

        Assert.AreEqual("Minst en gäst måste anges.", ex.Message);
    }
}
