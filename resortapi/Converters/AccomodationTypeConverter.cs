using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class AccomodationTypeConverter
    {
        public AccomodationTypeDto FromObjectToDto(AccomodationType type)
        {
            return new AccomodationTypeDto
            {
                Id = type.Id,
                Name = type.Name,
                Description = type.Description,
                BasePrice = type.BasePrice
            };
        }

        public AccomodationType FromDtoToObject(AccomodationTypeDto dto)
        {
            return new AccomodationType
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                BasePrice = dto.BasePrice
            };
        }

        public ICollection<AccomodationTypeDto> FromObjectCollection_ToOverviewCollection(ICollection<AccomodationType> types)
        {
            return types.Select(FromObjectToDto).ToList();
        }
    }
}
