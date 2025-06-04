using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resortapi.Converters;
using resortapi.Data;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class AccomodationService : IAccomodationService
    {
        private readonly IAccomodationRepo _repo;
        private readonly IConverter<Accomodation, AvailableRoomDto> _converter;
        private readonly ResortContext _context;

        public AccomodationService(IAccomodationRepo repo, IConverter<Accomodation, AvailableRoomDto> converter, ResortContext context)
        {
            _repo = repo;
            _converter = converter;
            _context = context;

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
        public ICollection<AvailableRoomDto> GetAvailableReceptionist(AvailableRoomRequest request)
        {
            var accomodations = _repo.GetAvailableByGuestNo(request.CheckIn, request.CheckOut, request.NoOfGuests).Result;

            var available = accomodations.Select(a => new AvailableRoomDto
            {
                Id = a.Id,
                Name = a.Name,
                AccomodationType = a.AccomodationType.Name,
                Description = a.AccomodationType.Description,
                MaxOccupancy = a.MaxOccupancy,
                BasePrice = a.AccomodationType.BasePrice,
                Accessibility = a.Accessibilities?.Select(acc => new AccessibilityDto
                {
                    Id = acc.Id,
                    Name = acc.Name,
                    Description = acc.Description
                }).ToList() ?? new List<AccessibilityDto>()
            }).ToList();

            return available;
        }
        public ICollection<AvailableRoomDto> GetAvailableGuest(AvailableRoomRequestExclGuests request)
        {
            var accomodations = _repo.GetAvailableAsync(request.CheckIn, request.CheckOut).Result;
            var available = accomodations.Select(a => new AvailableRoomDto
            {
                Id = a.Id,
                Name = a.Name,
                AccomodationType = a.AccomodationType.Name,
                Description = a.AccomodationType.Description,
                MaxOccupancy = a.MaxOccupancy,
                BasePrice = a.AccomodationType.BasePrice,
                Accessibility = a.Accessibilities?.Select(acc => new AccessibilityDto
                {
                    Id = acc.Id,
                    Name = acc.Name,
                    Description = acc.Description
                }).ToList() ?? new List<AccessibilityDto>()
            }).ToList();
            return available;
        }
        public async Task<ICollection<AvailableRoomDto>> GetAllAccomodations()
        {
            var accomodations = await _repo.GetAllAsync();
            return _converter.FromObjectCollection_ToOverviewCollection(accomodations);
        }
        public async Task<AvailableRoomDto?> GetAccomodationById(int id)
        {
            var accomodation = await _repo.GetByIdAsync(id);
            if (accomodation == null)
            {
                return null;
            }
            return _converter.FromObjecttoDTO(accomodation);

        }
        public async Task<AvailableRoomDto?> AddAccomodation(AccomodationDto newAccomodation)
        {
            var converter = _converter as AccomodationConverter;
            var accomodation = converter.FromDTOtoObject(newAccomodation, _context);
           
            var addedAccomodation = await _repo.AddAsync(accomodation);
            return converter.FromObjecttoDTO(addedAccomodation);
            
        }
        public async Task<AvailableRoomDto> UpdateAccomodation(int id, [FromBody] AccomodationDto updatedAccomodation)
        {
            var existingAccomodation = await _repo.GetByIdAsync(id);

            existingAccomodation.Name = updatedAccomodation.Name;
            existingAccomodation.MaxOccupancy = updatedAccomodation.MaxOccupancy;
            existingAccomodation.AccomodationTypeId = updatedAccomodation.AccomodationTypeId;
            existingAccomodation.Accessibilities.Clear();

            foreach (var accessibilityId in updatedAccomodation.AccessibilityIds)
            {
                var accessibility = await _context.Accessibilities.FindAsync(accessibilityId);
                if (accessibility != null)
                {
                    existingAccomodation.Accessibilities.Add(accessibility);
                }
            }
            var update = await _repo.UpdateAsync(existingAccomodation);
            return _converter.FromObjecttoDTO(update);
        }
        public async Task<bool> DeleteAccomodation(int id)
        {
            var existingAccomodation = await _repo.GetByIdAsync(id);
            if (existingAccomodation == null)
            {
                return false;
            }

            await _repo.DeleteAsync(existingAccomodation);
            return true;

        }
    }
}
