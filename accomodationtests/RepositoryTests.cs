using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mono.Cecil.Cil;
using restortlibrary.Data;
using restortlibrary.Models;
using restortlibrary.Repositories;
using System;

namespace accomodationtests;

[TestClass]
public class RepositoryTests
{

    DbContextOptionsBuilder<ResortContext> builder;
    DbContextOptions<ResortContext> options;
    ResortContext _context;
    public RepositoryTests()
    {
        builder = new DbContextOptionsBuilder<ResortContext>();
            builder.UseInMemoryDatabase("ResortContext");
        options = builder.Options;
        _context = new ResortContext(options);



    }

    [TestInitialize]
    public void TestInit()
    {
        var customer = new Customer() { Id = 1, FirstName = "Gustav", LastName = "Eriksson", 
                                        Email = "vd@swedbonk.se", Phone = "070000000", Type="VIP", 
                                        PaymentMethod = "Gold card", Bookings = new List<Booking>() };
        var customer2 = new Customer() { Id = 2, FirstName = "Alf",
            LastName = "Alfsson",
            Email = "stud@hotmail.se",
            Phone = "070d00000",
            Type = "Vanlig",
            PaymentMethod = "SMS Loan",
            Bookings = new List<Booking>() };
        var customer3 = new Customer() { Id = 3, FirstName = "Stefan",
            LastName = "Stefansson",
            Email = "steffe@edu.varberg.se",
            Phone = "070000001",
            Type = "Vanlig",
            PaymentMethod = "Cash",
            Bookings = new List<Booking>() };

        var booking = new Booking()
        {
            Id = 1,
            CheckIn = new DateTime(2025, 5, 25),
            CheckOut = new DateTime(2025, 5, 27),
            Customer = customer,
            Active = true,
            TimeOfBooking = new DateTime(2025, 5, 10),
            CancelationDate = new DateTime(2025, 5, 18),
            Cost = 1000,
            AmountPaid = 1000,
            Accomodation = new Accomodation()

        };
        _context.Set<Customer>().Add(customer);
        _context.Set<Customer>().Add(customer2);
        _context.Set<Customer>().Add(customer3);
        _context.Set<Booking>().Add(booking);
        _context.SaveChanges();
    }

    // Customer Repo
    // CRUD Tests

    [TestMethod]
    public void CustomerRepo_AddNewCustomer()
    {
        // Given a newly created customer being added 
        var expected = new Customer() { Id = 4, FirstName = "Kurt" };
        _context.Set<Customer>().Add(expected);
        // When checking if this customer is inserted in db
        var actual = _context.Set<Customer>().Find(4);
        // Then customer should be returned
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CustomerRepo_UpdateExistingCustomer()
    {
        // Given a customer in database
        var expected = _context.Set<Customer>().Find(1);
        // When updating first name of customer and saving
        expected.FirstName = "Test";
        _context.Set<Customer>().Update(expected);
        var actual = _context.Set<Customer>().Find(1);
        // Then customers name should be updated.

        Assert.AreEqual(expected, actual);

    }

    [TestMethod]
    public void CustomerRepo_DeleteCustomer()
    {
        // Given a customer in database
        var deleteme = _context.Set<Customer>().Find(1);

        // When trying to delete this customer
        _context.Set<Customer>().Remove(deleteme);

        // Then should not be found in database
        var actual = _context.Set<Customer>().Find(1);

        Assert.IsNull(actual);

    }

    [TestMethod]
    public void CustomerRepo_GetCustomerById()
    {
        // Given a customer in database


        // When searching for customer by id


        // Then should find this customer

        
    }

    [TestMethod]
    public void CustomerRepo_GetAllCustomers()
    {

        // Given several customers in database
        var actual = _context.Set<Customer>().ToList();

        // When getting all customers from database
        var expected = 3;

        // Then should find list of all customers
        Assert.AreEqual(expected, actual.Count);
   

    }

    // Booking Repo
    // Validation tests

    [TestMethod]
    public void CustomerBooking_CantBooKMoreThanOneStayAtTheSameDate()
    {
        
        // Given a booking between X dates
        var customer = _context.Set<Customer>().Find(1);
        var booking = new Booking() { Id = 1, CheckIn = new DateTime(2025, 5, 7), CheckOut = new DateTime(2025, 5, 09), Customer = customer, Active = true};

        // When trying to create a new booking for same customer between these dates
        var newBooking = new Booking() { Id = 2, CheckIn = new DateTime(2025, 5, 8), CheckOut = new DateTime(2025, 5, 22), Customer = customer, Active = true };

        if(booking.Customer == newBooking.Customer && newBooking.CheckIn < booking.CheckOut && booking.CheckIn <  newBooking.CheckOut)
        {
            newBooking = null;
        }

        // Then it should not be created
        Assert.IsNull(newBooking);
    }

    [TestMethod]
    public void CustomerBooking_CantBookBookedRoom()
    {
        // Given a booking request of a room
        var acco = new Accomodation() { Id = 1 };
        var booking = new Booking() { Id = 1, CheckIn = new DateTime(2025, 5, 7), CheckOut = new DateTime(2025, 5, 09), Active = true, Accomodation = acco};
        acco.Bookings.Add(booking);
        // When trying to do a new booking of same room during same dates
        var newBooking = new Booking() { Id = 2, CheckIn = new DateTime(2025, 5, 8), CheckOut = new DateTime(2025, 5, 22), Active = true };
       
        foreach(Booking b in acco.Bookings)
        {

            if (newBooking.CheckIn < b.CheckOut && b.CheckIn < newBooking.CheckOut)
            {
                newBooking = null;
            }
        }

        // Then should not work
        Assert.IsNull (newBooking);

    }

    [TestCleanup]
    public void Cleanup()
    {

    }
}
