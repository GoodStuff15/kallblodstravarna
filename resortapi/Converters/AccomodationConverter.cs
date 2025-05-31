using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class AccomodationConverter : IConverter<Accomodation, AvailableRoomDto>
    {
        public Accomodation FromDTOtoObject(AvailableRoomDto dto)
        {
            throw new NotImplementedException();
        }

        public ICollection<Accomodation> FromDTOtoObject_Collection(ICollection<AvailableRoomDto> collection)
        {
            throw new NotImplementedException();
        }

        public ICollection<AvailableRoomDto> FromObjectCollection_ToOverviewCollection(ICollection<Accomodation> accomodations)
        {
            var overview = new List<AvailableRoomDto>();

            foreach(var a in accomodations)
            {
                var dto = new AvailableRoomDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    AccomodationType = a.AccomodationType.Name,
                    Description = a.AccomodationType.Description,
                    MaxOccupancy = a.MaxOccupancy,
                    BasePrice = a.AccomodationType.BasePrice,
                    Accessibility = a.Accessibilities.Select(acc => new AccessibilityDto
                    {
                        Name = acc.Name,
                        Description = acc.Description
                    }).ToList()
                };
                overview.Add(dto);
            }
            return overview;
        }

        public AvailableRoomDto FromObjecttoDTO(Accomodation obj)
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
            };
        }

        public ICollection<AvailableRoomDto> FromObjecttoDTO_Collection(ICollection<Accomodation> collection)
        {
            throw new NotImplementedException();
        }
    }
}
