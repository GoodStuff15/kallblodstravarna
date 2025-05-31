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
        _options = new DbContextOptionsBuilder<ResortContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        //_options = new DbContextOptionsBuilder<ResortContext>()
        //    .UseInMemoryDatabase("ResortContext")
        //    .Options;
        _context = new ResortContext(_options);

        //_context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        SeedData();

        _repository = new AccomodationRepo(_context);
        _controller = new AccomodationController(_repository, new AccomodationConverter(),_context);

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
    [TestMethod]
    public async Task GetAccomodationById_WithValidData_ReturnAccomodationById()
    {
        // Arrange
        var readAccomodations = await _controller.GetAllAccomodations();

        var firstAcco = readAccomodations.Result as OkObjectResult;
        var accResult = firstAcco.Value as ICollection<AvailableRoomDto>;
        var id = accResult.First().Id;
        
        // Act
        var result = await _controller.GetAccomodationById(id);
       
        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var availableRoomDto = okResult.Value as AvailableRoomDto;
        Assert.IsNotNull(availableRoomDto);
        Assert.AreEqual(id, availableRoomDto.Id);
    }
    [TestMethod]
    public async Task GetAvailableAccomodations_WithValidData_ReturnsOnlyAvailableAccomodations()
    {
        // Arrange
        var checkIn = DateTime.Now.AddDays(1);
        var checkOut = DateTime.Now.AddDays(4);
        var noOfGuests = 2;
        
        // Act
        var result = await _controller.GetAvailableAccomodations(checkIn, checkOut, noOfGuests);
        
        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var availableRooms = okResult.Value as ICollection<AvailableRoomDto>;
        Assert.IsNotNull(availableRooms);
        Assert.AreEqual(2, availableRooms.Count);
    }
    [TestMethod]
    public async Task AddNewAccomodation_WithValidData_ReturnsCreatedAccomodation()
    {
        // Arrange
        var newAccomodation = new AccomodationDto
        {
            Name = "207C",
            MaxOccupancy = 4,
            AccomodationTypeId = _context.AccomodationTypes.First().Id
        };
        // Act
        var result = await _controller.AddNewAccomodation(newAccomodation);
        // Assert

        var createdResult = result as CreatedAtRouteResult;
        Assert.IsNotNull(createdResult);

        var createdAccomodation = createdResult.Value as AvailableRoomDto;
        Assert.IsNotNull(createdAccomodation);
        Assert.AreEqual(newAccomodation.Name, createdAccomodation.Name);
        Assert.AreEqual(newAccomodation.MaxOccupancy, createdAccomodation.MaxOccupancy);
    }
    [TestMethod]
    public async Task UpdateAccomodation_WithValidData_ReturnsUpdatedAccomodation()
    {
        // Arrange
        var readAccomodations = await _controller.GetAllAccomodations();

        var firstAcco = readAccomodations.Result as OkObjectResult;
        var accResult = firstAcco.Value as ICollection<AvailableRoomDto>;
        var id = accResult.First().Id;
        Assert.IsTrue(id > 0);

        var newAccomodation = new AccomodationDto
        {
            Name = "202B",
            MaxOccupancy = 3,
            AccomodationTypeId = _context.AccomodationTypes.First().Id,
            AccessibilityIds = new List<int> { 1, 2 }
        };
        // Act
        var result = await _controller.UpdateAccomodation(id, newAccomodation);
        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var updatedAccomodation = okResult.Value as AvailableRoomDto;
        Assert.IsNotNull(updatedAccomodation);

        Assert.AreEqual(newAccomodation.Name, updatedAccomodation.Name);
        Assert.AreEqual(newAccomodation.MaxOccupancy, updatedAccomodation.MaxOccupancy);

        // test accomodationTypeId with accomodationType name
        var name = _context.AccomodationTypes.FirstOrDefault(a => a.Id == newAccomodation.AccomodationTypeId)?.Name;
        Assert.AreEqual(name, updatedAccomodation.AccomodationType);
        Assert.AreEqual(2, updatedAccomodation.Accessibility.Count);
    }
    [TestMethod]
    public async Task DeleteAccomodation_WithValidData_DeletesAccomodation()
    {
        // arrange 
        var getAccomodation = _context.Accommodations.First();
        var deleteId = getAccomodation.Id;

        // Act
        var result = await _controller.DeleteAccomodation(deleteId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual($"Accomodation {deleteId} deleted successfully", okResult.Value);
        var deletedAcco = await _context.Accommodations.FindAsync(deleteId);
        Assert.IsNull(deletedAcco);
    }

}
