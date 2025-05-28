using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using resortapi.Converters;
using resortapi.Data;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class CalculatorServiceTests
{

    DbContextOptionsBuilder<ResortContext> builder;
    DbContextOptions<ResortContext> options;
    ResortContext _context;
    public CalculatorServiceTests()
    {
        builder = new DbContextOptionsBuilder<ResortContext>();
        builder.UseInMemoryDatabase("ResortContext");
        options = builder.Options;
        _context = new ResortContext(options);
        var option = new AdditionalOption() { Id = 1, Price = 120, Name = "Breakfast", Description ="Buffé"};
        var accotype = new AccomodationType() { Id = 1, BasePrice = 1250, Name = "Rum" };
        var acco = new Accomodation() { Id = 1, AccomodationType = accotype, AccomodationTypeId = 1, Name = "Rum 1", MaxOccupancy = 4 };
        var guest1 = new Guest() { Id = 1, FirstName = "Gustav", LastName = "Eriksson", Age = 25 };
        var guest2 = new Guest() { Id = 2, FirstName = "Gustava", LastName = "Eriksdottir", Age = 30 };

 
        _context.Set<Booking>().Add(new Booking() { Id = 1, AdditionalOptions = new List<AdditionalOption>() { option }, 
                              Guests = new List<Guest>() { guest1, guest2 }, Accomodation = acco });
        _context.SaveChanges();
    }

    [TestMethod]
    public void CalculatePriceFromBooking()
    {
        // Given a booking with accomodation and additional options
        var booking = _context.Set<Booking>().Find(1);

        // When calculating total price
        decimal actual = 0;
        foreach (var option in booking.AdditionalOptions)
        {
            actual += option.Price;
        }
        
        actual += booking.Accomodation.AccomodationType.BasePrice;

        booking.Cost = actual;

        Assert.IsNotNull(booking);
        Assert.AreEqual(1370, actual);
    }

    [TestMethod] 
    public void CalculatePriceFromBookingDto()
    {
        decimal totalPrice = 0;
        var booking = _context.Set<Booking>().Find(1);
        var conv = new BookingConverter();
        var dto = conv.FromObjecttoDTO(booking);
        var additional = new List<AdditionalOption>();
        foreach( var option in dto.AdditionalOptionIds)
        {
            additional.Add(_context.Set<AdditionalOption>().Find(option));
        }

        var accomodation = _context.Set<Accomodation>().Find(dto.AccomodationId);
        var type = _context.Set<AccomodationType>().Find(accomodation.AccomodationTypeId);
        foreach ( var option in additional)
        {
            totalPrice += option.Price;
        }

        totalPrice += type.BasePrice;

        Assert.AreEqual(1370, totalPrice);

    }
}
