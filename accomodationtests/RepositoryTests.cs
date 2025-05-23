//using Microsoft.EntityFrameworkCore;
//using resortapi.Data;
//using resortlibrary.Models;

//namespace accomodationtests;

//[TestClass]
//public class RepositoryTests
//{

//    DbContextOptionsBuilder<ResortContext> builder;
//    DbContextOptions<ResortContext> options;
//    ResortContext _context;
//    public RepositoryTests()
//    {
//        builder = new DbContextOptionsBuilder<ResortContext>();
//            builder.UseInMemoryDatabase("ResortContext");
//        options = builder.Options;
//        _context = new ResortContext(options);



//    }

//    [TestInitialize]
//    public void TestInit()
//    {
//        builder = new DbContextOptionsBuilder<ResortContext>();
//        builder.UseInMemoryDatabase("ResortContext");
//        options = builder.Options;
//        _context = new ResortContext(options);



//        var customer = new Customer() { Id = 1, FirstName = "Gustav", LastName = "Eriksson", 
//                                        Email = "vd@swedbonk.se", Phone = "070000000", Type="VIP", 
//                                        PaymentMethod = "Gold card", Bookings = new List<Booking>() };
//        var customer2 = new Customer() { Id = 2, FirstName = "Alf",
//            LastName = "Alfsson",
//            Email = "stud@hotmail.se",
//            Phone = "070d00000",
//            Type = "Vanlig",
//            PaymentMethod = "SMS Loan",
//            Bookings = new List<Booking>() };
//        var customer3 = new Customer() { Id = 3, FirstName = "Stefan",
//            LastName = "Stefansson",
//            Email = "steffe@edu.varberg.se",
//            Phone = "070000001",
//            Type = "Vanlig",
//            PaymentMethod = "Cash",
//            Bookings = new List<Booking>() };

//        var booking = new Booking()
//        {
//            Id = 1,
//            CheckIn = new DateTime(2025, 5, 25),
//            CheckOut = new DateTime(2025, 5, 27),
//            Customer = customer,
//            Active = true,
//            TimeOfBooking = new DateTime(2025, 5, 10),
//            CancellationDate = new DateTime(2025, 5, 18),
//            Cost = 1000,
//            AmountPaid = 1000,
//            Accomodation = new Accomodation() { Id = 1, MaxOccupancy = 2 }

//        };
//        var booking2 = new Booking()
//        {
//            Id = 2,
//            CheckIn = new DateTime(2025, 5, 12),
//            CheckOut = new DateTime(2025, 5, 15),
//            Customer = customer2,
//            Active = true,
//            TimeOfBooking = new DateTime(2025, 5, 5),
//            CancellationDate = new DateTime(2025, 5, 5),
//            Cost = 1000,
//            AmountPaid = 0,
//            Accomodation = new Accomodation() { Id = 2, MaxOccupancy = 4 }

//        };
//        var booking3 = new Booking()
//        {
//            Id = 3,
//            CheckIn = new DateTime(2025, 5, 14),
//            CheckOut = new DateTime(2025, 5, 26),
//            Customer = customer3,
//            Active = true,
//            TimeOfBooking = new DateTime(2025, 5, 10),
//            CancellationDate = new DateTime(2025, 5, 18),
//            Cost = 1000,
//            AmountPaid = 1000,
//            Accomodation = new Accomodation() { Id = 3, MaxOccupancy = 4}


//        };


//        var accomodationType = new AccomodationType() { Id = 2, Name = "Rum", BasePrice = 1000 };

       
//        _context.Set<AccomodationType>().Add(accomodationType);
//        var accomodation = new Accomodation() { Id = 2, Name = "Gamerrummet", MaxOccupancy = 4};
//        _context.Set<Accomodation>().Add(accomodation);

//        _context.Set<Customer>().Add(customer);
//        _context.Set<Customer>().Add(customer2);
//        _context.Set<Customer>().Add(customer3);
//        _context.Set<Booking>().Add(booking);
//        _context.Set<Booking>().Add(booking2);
//        _context.Set<Booking>().Add(booking3);
//        _context.Set<Accomodation>().Add(accomodation);
//        _context.SaveChanges();
//    }

//    // Customer Repo
//    // CRUD Tests

//    [TestMethod]
//    public void CustomerRepo_AddNewCustomer()
//    {
//        // Given a newly created customer being added 
//        var expected = new Customer() { Id = 4, FirstName = "Kurt" };
//        _context.Set<Customer>().Add(expected);
//        // When checking if this customer is inserted in db
//        var actual = _context.Set<Customer>().Find(4);
//        // Then customer should be returned
//        Assert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void CustomerRepo_UpdateExistingCustomer()
//    {
//        // Given a customer in database
//        var expected = _context.Set<Customer>().Find(1);
//        // When updating first name of customer and saving
//        expected.FirstName = "Test";
//        _context.Set<Customer>().Update(expected);
//        var actual = _context.Set<Customer>().Find(1);
//        // Then customers name should be updated.

//        Assert.AreEqual(expected, actual);

//    }

//    [TestMethod]
//    public void CustomerRepo_DeleteCustomer()
//    {
//        // Given a customer in database
//        var deleteme = _context.Set<Customer>().Find(1);

//        // When trying to delete this customer
//        _context.Set<Customer>().Remove(deleteme);

//        // Then should not be found in database
//        var actual = _context.Set<Customer>().Find(1);

//        Assert.IsNull(actual);

//    }

//    [TestMethod]
//    public void CustomerRepo_GetCustomerById()
//    {
//        // Given a customer in database


//        // When searching for customer by id


//        // Then should find this customer

        
//    }

//    [TestMethod]
//    public void CustomerRepo_GetAllCustomers()
//    {

//        // Given several customers in database
//        var actual = _context.Set<Customer>().ToList();

//        // When getting all customers from database
//        var expected = 3;

//        // Then should find list of all customers
//        Assert.AreEqual(expected, actual.Count);
   

//    }

//    // Booking Repo
//    // Validation tests

//    [TestMethod]
//    public void CustomerBooking_CantBooKMoreThanOneStayAtTheSameDate()
//    {
        
//        // Given a booking between X dates
//        var customer = _context.Set<Customer>().Find(1);
//        var booking = new Booking() { Id = 1, CheckIn = new DateTime(2025, 5, 7), CheckOut = new DateTime(2025, 5, 09), Customer = customer, Active = true};

//        // When trying to create a new booking for same customer between these dates
//        var newBooking = new Booking() { Id = 2, CheckIn = new DateTime(2025, 5, 8), CheckOut = new DateTime(2025, 5, 22), Customer = customer, Active = true };

//        if(booking.Customer == newBooking.Customer && newBooking.CheckIn < booking.CheckOut && booking.CheckIn <  newBooking.CheckOut)
//        {
//            newBooking = null;
//        }

//        // Then it should not be created
//        Assert.IsNull(newBooking);
//    }

//    [TestMethod]
//    public void CustomerBooking_CantBookBookedRoom()
//    {
//        // Given a booking request of a room
//        var acco = new Accomodation() { Id = 1 };
//        var booking = new Booking() { Id = 1, CheckIn = new DateTime(2025, 5, 7), CheckOut = new DateTime(2025, 5, 09), Active = true, Accomodation = acco};
//        acco.Bookings.Add(booking);
//        // When trying to do a new booking of same room during same dates
//        var newBooking = new Booking() { Id = 2, CheckIn = new DateTime(2025, 5, 8), CheckOut = new DateTime(2025, 5, 22), Active = true };
       
//        foreach(Booking b in acco.Bookings)
//        {

//            if (newBooking.CheckIn < b.CheckOut && b.CheckIn < newBooking.CheckOut)
//            {
//                newBooking = null;
//            }
//        }

//        // Then should not work
//        Assert.IsNull (newBooking);
//    }

//    [TestMethod]
//    public void CustomerBooking_CantBookDatesInThePast()
//    {
//        // If trying to add booking with dates in the past
//        var booking = new Booking() { Id = 3, CheckIn = new DateTime(2025, 5, 22), CheckOut = new DateTime(2025, 5, 20), Active = true};

//        // When checking if dates are in the past
//        if(booking.CheckIn < DateTime.Now || booking.CheckOut < DateTime.Now)
//        {
//            booking = null;
//        }

//        // Then booking should not be created
//        Assert.IsNull(booking);
//    }

//    [TestMethod]
//    public void CustomerBooking_CantPutCheckOutOnSameOrEarlierDayThanCheckIn()
//    {
//        var booking = new Booking() { Id = 3, CheckIn = new DateTime(2025, 5, 22), CheckOut = new DateTime(2025, 5, 21), Active = true};

//        if(booking.CheckOut <= booking.CheckIn)
//        {
//            booking = null;
//        }
//        Assert.IsNull(booking);
//    }

//    [TestMethod]
//    public void CustomerBooking_NumberOfGuests_IsBiggerThanZero()
//    {
//        var booking = new Booking() { Id = 3, CheckIn = new DateTime(2025, 5, 22), CheckOut = new DateTime(2025, 5, 21), Active = true };
//        booking.Guests.Add(new Guest());
//        if(booking.Guests.Count == 0)
//        {
//            booking = null;
//        }
//        Assert.IsNull(booking);
//    }
//    [TestMethod]
//    public void CustomerBooking_NumberOfGuests_CantBeBiggerThanAccomodationSize()
//    {
//        // When given a booking with number of guests bigger than accomodation capacity
//        var booking = new Booking() { Id = 3, CheckIn = new DateTime(2025, 5, 22), CheckOut = new DateTime(2025, 5, 21), Active = true };
//        booking.Accomodation = new Accomodation() { MaxOccupancy = 4 };
//        booking.Guests.Add(new Guest());
//        booking.Guests.Add(new Guest());
//        booking.Guests.Add(new Guest());
//        booking.Guests.Add(new Guest());
//        booking.Guests.Add(new Guest());

//        // when comparing max occupancy to guest count
//        if (booking.Accomodation.MaxOccupancy < booking.Guests.Count)
//        {
//            booking = null;
//        }

//        // Booking should fail
//        Assert.IsNull(booking);
//    }
//    [TestMethod]
//    public void CustomerBooking_CalculatingPrice()
//    {
//        var booking = new Booking() { Id = 3, CheckIn = new DateTime(2025, 5, 17), CheckOut = new DateTime(2025, 5, 21), Active = true, CancellationDate = new DateTime(2025, 5, 10)};
//        booking.Accomodation = _context.Set<Accomodation>().Find(2);
//        booking.Accomodation.AccomodationType = _context.Set<AccomodationType>().Find(2);
//        booking.Cost = booking.Accomodation.AccomodationType.BasePrice;
//        decimal extra = booking.AdditionalOptions
//                        .Select(x => x.Price)
//                        .Sum();

//        var daysOfStay = new TimeSpan(booking.CheckOut.Ticks - booking.CheckIn.Ticks);

//        var deezNuts = daysOfStay.TotalDays;

//        var actual = booking.Cost * (decimal)deezNuts;
//        Assert.AreEqual(4000, actual);

//    }

//    [TestMethod]
//    public void CustomerBooking_CancellingABooking_HasToBeBeforeCancellationDate()
//    {
//        // Given a booking with a cancellationdate that has passed
//        var booking = new Booking() { Id = 3, CheckIn = new DateTime(2025, 5, 17), CheckOut = new DateTime(2025, 5, 21), Active = true, CancellationDate = new DateTime(2025, 5, 10) };

//        // When checking if cancellation is avaialble at this date
//        if(booking.CancellationDate > DateTime.Now)
//        {
//            booking.Active = false;
//        }
//        // Then booking should remain active
//        Assert.IsTrue(booking.Active);
//    }

//    [TestMethod]
//    public void Accomodations_SeeAllAvailableInDateRange()
//    {

//        var start = new DateTime(2025, 5, 10);
//        var end = new DateTime(2025, 5, 15);

//        var availableRooms = _context.Set<Booking>()
//                            .Where(b => b.CheckIn < end && start < b.CheckOut)
//                            .Select(a => a.Accomodation)
//                            .ToList();
        

//        Assert.AreEqual(2, availableRooms.Count);
//    }

//    [TestMethod]
//    public void Accomodations_SeeAllAvailableByDateAndCapacity()
//    {
//        var start = new DateTime(2025, 5, 1);
//        var end = new DateTime(2025, 5, 13);

//        var noOfGuests = 3;

//        var availableRooms = _context.Set<Accomodation>()
//                             .Where(a => a.MaxOccupancy >= noOfGuests)
//                             .Include(a => a.Bookings)
//                             .SelectMany(
//                                 acc => acc.Bookings
//                                .Where(date => !(start < date.CheckOut && date.CheckIn < end))
//                                .Select(a => acc))
//                             .ToList();
                              


//        Assert.AreEqual(1, availableRooms.Count);
    
//    }

//    [TestCleanup]
//    public void Cleanup()
//    {

//    }
//}
