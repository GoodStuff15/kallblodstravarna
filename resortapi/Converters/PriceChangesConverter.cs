using resortlibrary.Models;

namespace resortapi.Converters
{
    public class PriceChangesConverter : IConverter<PriceChanges, PriceChangesDto>
    {
        public PriceChanges FromDTOtoObject(PriceChangesDto dto)
        {
            var obj = new PriceChanges
            {
                Id = dto.Id,
                PriceChange = dto.PriceChange,
                Type = dto.Type
            };
            return obj;
        }
        public ICollection<PriceChanges> FromDTOtoObject_Collection(ICollection<PriceChangesDto> collection)
        {
            throw new NotImplementedException();
        }
        public PriceChangesDto FromObjecttoDTO(PriceChanges obj)
        {
            var dto = new PriceChangesDto
            {
                Id = obj.Id,
                PriceChange = obj.PriceChange,
                Type = obj.Type
            };
            return dto;
        }
        public ICollection<PriceChangesDto> FromObjecttoDTO_Collection(ICollection<PriceChanges> collection)
        {
            return collection.Select(pc => new PriceChangesDto
            {
                Id = pc.Id,
                PriceChange = pc.PriceChange,
                Type = pc.Type
            }).ToList();
        }
    }
}