using Microsoft.EntityFrameworkCore;
using resortapi.Controllers;
using resortapi.Data;
using resortapi.Repositories;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class AccomodationControllerTest
{
    private DbContextOptions<ResortContext> _options;
    private ResortContext _context;
    private AccomodationController _controller;
    private AccomodationRepo _repository;

    public void SeedData()
    {
        var accomodationType1 = new AccomodationType { Name = "Enkelrum", Description = "Ett rum med en säng", BasePrice = 800 };
        var accomodationType2 = new AccomodationType { Name = "Dubbelrum", Description = "Rum med två sängar", BasePrice = 1200 };
        var accomodationType3 = new AccomodationType { Name = "Svit", Description = "Lyxsvit med utsikt", BasePrice = 2500 };

        _context.AccomodationTypes.AddRange(accomodationType1, accomodationType2, accomodationType3);
        _context.SaveChanges();

        var accomodation = new List<Accomodation>
        {
            new Accomodation {Name = "101A", MaxOccupancy = 1, AccomodationTypeId = accomodationType1.Id},
            new Accomodation {Name = "202B", MaxOccupancy = 2, AccomodationTypeId = accomodationType2.Id},
            new Accomodation {Name = "Penthouse 1", MaxOccupancy = 4, AccomodationTypeId = accomodationType3.Id}
        };
        _context.Accommodations.AddRange(accomodation);
        _context.SaveChanges();

    }



    [TestMethod]
    public void TestMethod1()
    {

    }
}
