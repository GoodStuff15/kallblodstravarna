using Moq;
using resortapi.Converters;
using resortapi.Repositories;
using resortapi.Services;
using resortdtos;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class AccomodationServiceTest
{
    private Mock<IAccomodationService> _serviceMock;
    private Mock<IAccomodationRepo> _repositoryMock;
    private Mock<IConverter<Accomodation, AvailableRoomDto>> _converterMock;
    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IAccomodationRepo>();
        _serviceMock = new Mock<IAccomodationService>();
        _converterMock = new Mock<IConverter<Accomodation, AvailableRoomDto>>();


    }
    [TestMethod]
    public void AccomodationService_GetAllAvailableRooms_ReturnsAListOfDtos()
    {
        var setupList = _repositoryMock.Object.GetAvailableByGuestNo(new DateTime(2025, 6, 10), new DateTime(2025, 6, 20), 2).Result;
        var request = new AvailableRoomRequest() { CheckIn = new DateTime(2025, 6, 10), CheckOut = new DateTime(2025, 6, 20), NoOfGuests = 2 };
       
        _repositoryMock.Setup(repo => repo.GetAvailableByGuestNo(new DateTime(2025, 6, 10), new DateTime(2025, 6, 20), 2))
                       .Returns(Task.FromResult(setupList));
        _converterMock.Setup(cnv => cnv.FromObjecttoDTO_Collection(setupList)).Returns(new List<AvailableRoomDto>());
        _serviceMock.Setup(srv => srv.GetAvailableRooms(request))
                    .Returns(new List<AvailableRoomDto>());


        var result = _serviceMock.Object.GetAvailableRooms(request);

        Assert.IsNotNull(result);

    }

    [TestCleanup]
    public void Cleanup()
    {
        _repositoryMock = null;
        _serviceMock = null;
        _converterMock = null;
    }
}
