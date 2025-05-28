using Microsoft.Identity.Client;
using resortapi.Converters;
using resortdtos;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class ConverterTests
{

    // Basic conversion of obj/dto and collections
    // Accessibility
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

    // Guest

    [TestMethod]
    public void FromGuest_ToGuestDTO()
    {
        var converter = new GuestConverter();
        var expected = new Guest() { Id = 1, FirstName = "Gurra", LastName = "Svensson", Age = 52 };

        var actual = converter.FromObjecttoDTO(expected);

        Assert.AreEqual(expected.FirstName, actual.FirstName);
        Assert.AreEqual (expected.LastName, actual.LastName);
        Assert.AreEqual(expected.Age, actual.Age);
    }

    [TestMethod]
    public void FromGuestDTO_ToGuest()
    {
        var converter = new GuestConverter();
        var expected = new GuestDto() { FirstName = "Gurra", LastName = "Svensson", Age = 52 };

        var actual = converter.FromDTOtoObject(expected);

        Assert.AreEqual(expected.FirstName, actual.FirstName);
        Assert.AreEqual(expected.LastName, actual.LastName);
        Assert.AreEqual(expected.Age, actual.Age);
    }

    [TestMethod]
    public void FromGuestCollection_ToGuestDTOCollection()
    {
        var converter = new GuestConverter();
        var guest1 = new Guest() { Id = 1, FirstName = "Sven", LastName = "Stefansson", Age = 3 };
        var guest2 = new Guest() { Id = 2, FirstName = "Stefan", LastName = "Svensson", Age = 29 }; 
        var expected = new List<Guest> { guest1, guest2 };
        
        var actual = converter.FromObjecttoDTO_Collection(expected).ToList();


        Assert.AreEqual(expected.Count, actual.Count);
        Assert.AreEqual(expected[0].FirstName, actual[0].FirstName);
        Assert.AreEqual(expected[1].FirstName, actual[1].FirstName);
        Assert.AreEqual(expected[0].Age, actual[0].Age);

    }

    // Booking

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
        dto.Guests = dtoguests;
        dto.AccomodationId = obj.AccomodationId;    


    }

    [TestMethod]
    public void BookingConverter_FromObjectCollection_ToDTOCollection()
    {
        var guests = new List<Guest>()
        {
            new Guest() { Id = 1, Age = 25, FirstName = "Gustav", LastName = "Eriksson"},
            new Guest() { Id = 2, Age = 33, FirstName = "Sven", LastName = "Svensson"},
            new Guest() { Id = 3, Age = 100, FirstName = "Kenta", LastName = "Olofsson"},
            new Guest() { Id = 4, Age = 99, FirstName = "Olof", LastName = "Kentaborg"},

        };
        var bookingList = new List<Booking>()
        {
            new Booking() { Id = 1, AccomodationId = 1, CheckIn = new DateTime(2025, 6, 1), CheckOut = new DateTime(2025, 6, 2), Guests = guests},
            new Booking() { Id = 2, AccomodationId = 2, CheckIn = new DateTime(2025, 7, 1), CheckOut = new DateTime(2025, 7, 5), Guests = guests}
        };

        var dtoList = new List<BookingDto>();
        var guestList = new List<GuestDto>();
        var conv = new BookingConverter();
        var testList = conv.FromObjecttoDTO_Collection(bookingList).ToList();
        
        foreach(var b in bookingList)
        {
            foreach(var g in b.Guests)
            {
                var guestDto = new GuestDto()
                {
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    Age = g.Age,
                };
                guestList.Add(guestDto);
            }

            var dto = new BookingDto()
            {
                AccomodationId = b.AccomodationId,
                CheckIn = b.CheckIn,
                CheckOut = b.CheckOut,
                CustomerId = b.CustomerId,
                //AdditionalOptionIds = b.AdditionalOptionIds (FIX FIX)
                Guests = guestList
            };

            dtoList.Add(dto);
            guestList.Clear();
        }
        var expected = bookingList.Count;
        Assert.AreEqual(expected, dtoList.Count);
        //CollectionAssert.AreEqual(bookingList[1].Guests.ToList(), testList[1].Guests.ToList());
        Assert.AreEqual(bookingList[1].Guests.ToList()[1].FirstName, testList[1].Guests.ToList()[1].FirstName);

    }


}
