using resortapi.Data;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class AccomodationConverter : IConverter<Accomodation, AccomodationDto>
    {
        public Accomodation FromDTOtoObject(AccomodationDto dto)
        {
            throw new NotImplementedException();
        }

        public ICollection<Accomodation> FromDTOtoObject_Collection(ICollection<AccomodationDto> collection)
        {
            throw new NotImplementedException();
        }

        public ICollection<AccomodationDto> FromObjectCollection_ToOverviewCollection(ICollection<Accomodation> accomodations)
        {
            var overview = new List<AccomodationDto>();

            foreach (var a in accomodations)
            {
                var dto = new AccomodationDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    AccomodationTypeName = a.AccomodationType.Name
                };
                overview.Add(dto);
            }

            return overview;
        }

        public AccomodationDto FromObjecttoDTO(Accomodation obj)
        {

            return new AccomodationDto()
            {
                Id = obj.Id,
                Name = obj.Name,
                MaxOccupancy = obj.MaxOccupancy,
                AccomodationTypeName = obj.AccomodationType.Name

            };
        }
        
        public AvailableRoomDto FromObject_ToAvailableRoomDto(Accomodation)
        {
        return new AvailableRoomDto()
            {
                Id = obj.Id,
                Name = obj.Name,
                AccomodationType = obj.AccomodationType?.Name ?? "", 
                Description = obj.AccomodationType?.Description ?? "", 
                MaxOccupancy = obj.MaxOccupancy,
                BasePrice = obj.AccomodationType?.BasePrice ?? 0,
                Accessibility = obj.Accessibilities?.Select(acc => new AccessibilityDto
                {
                    Name = acc.Name,
                    Description = acc.Description
                }).ToList() ?? new List<AccessibilityDto>()
        }

        public ICollection<AccomodationDto> FromObjecttoDTO_Collection(ICollection<Accomodation> collection)
        {
            return collection.Select(a => FromObjecttoDTO(a)).ToList();
        }
        public Accomodation FromDTOtoObject(AccomodationDto dto, ResortContext context)
        {
            var accomodation = new Accomodation
            {
                Name = dto.Name,
                MaxOccupancy = dto.MaxOccupancy,
                AccomodationTypeId = dto.AccomodationTypeId
            };
            // return accessibilityIds for accomodation
            foreach (var accessibilityId in dto.AccessibilityIds)
            {
                var accessibility = context.Accessibilities.Find(accessibilityId);
                if (accessibility != null)
                {
                    accomodation.Accessibilities.Add(accessibility);
                }
            }
            return accomodation;
        }
    }
}
