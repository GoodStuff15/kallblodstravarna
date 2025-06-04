using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public interface IAccomodationService
    {
        public bool ValidateAccomodation(Accomodation accomodation);
        public AvailableRoomDto ConvertToAvailableRoomDto(Accomodation accomodation);
        public ICollection<AvailableRoomDto> GetAvailableRooms(AvailableRoomRequest request);

        public ICollection<AvailableRoomDto> GetAvailableReceptionist(AvailableRoomRequest request);
        public ICollection<AvailableRoomDto> GetAvailableGuest(AvailableRoomRequestExclGuests request);
        public Task<ICollection<AvailableRoomDto>> GetAllAccomodations();
        public Task<AvailableRoomDto?> GetAccomodationById(int id);
        public Task<AvailableRoomDto?> AddAccomodation(AccomodationDto newAccomodation);
        public Task<AvailableRoomDto?> UpdateAccomodation(int id, AccomodationDto updatedAccomodation);
        public Task<bool> DeleteAccomodation(int id);
    }
}

