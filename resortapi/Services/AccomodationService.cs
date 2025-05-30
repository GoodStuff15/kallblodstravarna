using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class AccomodationService : IAccomodationService
    {
        private readonly AccomodationRepo _repo;
        private readonly IConverter<Accomodation, AvailableRoomDto> _converter;

        public AccomodationService(AccomodationRepo repo, IConverter<Accomodation, AvailableRoomDto> converter)
        {
            _repo = repo;
            _converter = converter;
        }

        public bool ValidateAccomodation(Accomodation accomodation)
        {
            // validate what?

            return true;
        }

        public AvailableRoomDto ConvertToAvailableRoomDto(Accomodation accomodation)
        {
            throw new NotImplementedException();
        }

        public ICollection<AvailableRoomDto> GetAvailableRooms(AvailableRoomRequest request)
        {
            var available = _repo.GetAvailableByGuestNo(request.CheckIn, request.CheckOut, request.NoOfGuests).Result;

            foreach (var a in available)
            {
                if (!ValidateAccomodation(a))
                {
                    throw new Exception($"Validation error: accomodation {a.Id}");
                }
            }

            return _converter.FromObjecttoDTO_Collection(available);
        }


    }
}
