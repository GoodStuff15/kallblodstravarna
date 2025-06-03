using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class AdditionalOptionConverter
    {
        public AdditionalOptionDto FromModelToDto(AdditionalOption option)
        {
            return new AdditionalOptionDto
            {
                Id = option.Id,
                Name = option.Name,
                Description = option.Description,
                Price = option.Price,
                PerGuest = option.PerGuest,
                PerNight = option.PerNight
            };
        }

        public AdditionalOption FromDtoToModel(AdditionalOptionDto dto)
        {
            return new AdditionalOption
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                PerGuest = dto.PerGuest,
                PerNight = dto.PerNight
            };
        }

        public ICollection<AdditionalOptionDto> FromModelCollectionToDto(ICollection<AdditionalOption> options)
        {
            return options.Select(FromModelToDto).ToList();
        }

        public ICollection<AdditionalOption> FromDtoCollectionToModel(ICollection<AdditionalOptionDto> dtos)
        {
            return dtos.Select(FromDtoToModel).ToList();
        }
    }
}
