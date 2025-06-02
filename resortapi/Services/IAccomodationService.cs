using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public interface IAccomodationService
    {
        public bool ValidateAccomodation(Accomodation accomodation);
        public AvailableRoomDto ConvertToAvailableRoomDto(Accomodation accomodation);
        public ICollection<AvailableRoomDto> GetAvailableRooms(AvailableRoomRequest request);
    
        
    }
}
