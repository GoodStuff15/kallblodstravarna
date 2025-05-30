using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resortapi.Controllers;
using resortapi.Converters;
using resortapi.Data;
using resortapi.Repositories;
using resortdtos;
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
        _context.Accommodations.RemoveRange(_context.Accommodations);
        _context.AccomodationTypes.RemoveRange(_context.AccomodationTypes);
        _context.SaveChanges();

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
    [TestInitialize]
    public void TestInit()
    {
        //_options = new DbContextOptionsBuilder<ResortContext>()
        //    .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //    .Options;
        _options = new DbContextOptionsBuilder<ResortContext>()
            .UseInMemoryDatabase("ResortContext")
            .Options;
        _context = new ResortContext(_options);

        //_context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        SeedData();

        _repository = new AccomodationRepo(_context);
        _controller = new AccomodationController(_repository, new AccomodationConverter());

    }
    [TestMethod]
    public async Task GetAllAccomodation_WithValidData_ReturnsAccomodations()
    {
        var result = await _controller.GetAllAccomodations();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var accomodations = okResult.Value as ICollection<AvailableRoomDto>;
        Assert.IsNotNull(accomodations);
        Assert.AreEqual(3, accomodations.Count);
    }

}
