using resortdtos;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class ConverterTests
{
    [TestMethod]
    public void FromAccessibility_ToAccessibilityDTO ()
    {
        var dto = new AccessibilityDto();

        var obj = new Accessibility() { Id = 1, Name = "test",  Description = "Ett test", Accomodations = new List<Accomodation>()};
    
        dto.Description = obj.Description;
        dto.Name = obj.Name;
        
        Assert.AreEqual(obj.Name, dto.Name);
        Assert.AreEqual(obj.Description, dto.Description);
    }

    [TestMethod]
    public void FromAccessibilityDTO_ToAccessibility()
    {
        var obj = new Accessibility();

        var dto = new AccessibilityDto() { Name = "test", Description = "Ett test"};

        obj.Description = dto.Description;
        obj.Name = dto.Name;

        Assert.AreEqual(obj.Name, dto.Name);
        Assert.AreEqual(obj.Description, dto.Description);
    }

    [TestMethod]
    public void FromBooking_ToBookingDTO()
    {
        var dto = new BookingDto();

        var obj = new Booking() { Guests = new List<Guest>(), Id = 1, Active = true, Cost = 1000, CheckIn = new DateTime(2025, 6, 1), CheckOut = new DateTime(2025, 6, 5) };
        var guest1 = new Guest() { Id = 1, Age = 25, FirstName = "Gustav", LastName = "Eriksson", Booking = obj};
        var guest2 = new Guest() { Id = 2, Age = 33, FirstName = "Sven", LastName = "Svensson", Booking = obj};

        obj.Guests.Add(guest1);
        obj.Guests.Add(guest2);

        var dtoguests = new List<GuestDto>();
        foreach(var g in obj.Guests)
        {
            var gdto = new GuestDto();
            gdto.Age = g.Age;
            gdto.FirstName = g.FirstName;
            gdto.LastName = g.LastName;
            dtoguests.Add(gdto);
        }

        dto.CheckIn = obj.CheckIn;
        dto.CheckOut = obj.CheckOut;
        dto.Cost = obj.Cost;
        dto.Guests = dtoguests;
        dto.AccomodationId = obj.AccomodationId;    


    }
}
