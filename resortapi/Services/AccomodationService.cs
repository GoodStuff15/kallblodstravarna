using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class AccomodationService : IAccomodationService
    {
        private readonly IAccomodationRepo _repo;
        private readonly IConverter<Accomodation, AvailableRoomDto> _converter;

        public AccomodationService(IAccomodationRepo repo, IConverter<Accomodation, AvailableRoomDto> converter)
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
            return _converter.FromObjecttoDTO(accomodation);
        }

        public ICollection<AvailableRoomDto> GetAvailableRooms(AvailableRoomRequest request)
        {
            var available = _repo.GetAvailableByGuestNo(request.CheckIn, request.CheckOut, request.NoOfGuests).Result;

            var validatedAndConverted = new List<AvailableRoomDto>();

            
            foreach (var a in available)
            {
                // validation
                if (!ValidateAccomodation(a))
                {
                    throw new Exception($"Validation error: accomodation {a.Id}");
                }

                //conversion
                validatedAndConverted.Add(ConvertToAvailableRoomDto(a));
            }

            return validatedAndConverted;
        }


    }
}
