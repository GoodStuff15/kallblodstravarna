using Microsoft.EntityFrameworkCore;
using resortapi.Data;
using resortapi.Repositories;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class ControllerTests
{
    DbContextOptionsBuilder<ResortContext> builder;
    DbContextOptions<ResortContext> options;
    ResortContext _context;
    BookingRepo _bookingRepo;
    public ControllerTests()
    {
        builder = new DbContextOptionsBuilder<ResortContext>();
        builder.UseInMemoryDatabase("ResortContext");
        options = builder.Options;
        _context = new ResortContext(options);
        _bookingRepo = new BookingRepo(_context);


    }

    [TestInitialize]
    public void TestInit()
    {
        _context.Set<Booking>().Add(new Booking() {  Id = 1 });   
        _context.Set<Booking>().Add(new Booking() {  Id = 2 });   
        _context.Set<Booking>().Add(new Booking() {  Id = 3 });
        _context.SaveChanges();
    }

    [TestMethod]
    public void BookingController_HttpGet_FindsAllBookings_WithAllInfoNeeded()
    {
        var actual = _bookingRepo.GetAllAsync().Result.ToList();
        var expected = _context.Set<Booking>().ToList();
        
        var actualCount = actual.Count;
        var expectedCount = expected.Count;

        Assert.AreEqual(expectedCount, actualCount);
        CollectionAssert.AreEqual(expected, actual);


    }

    [TestCleanup]
    public void TestCleanup()
    {

    }
}

