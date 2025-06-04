
using resortdtos;
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

        public ICollection<PriceChangesDto> FromObjectCollection_ToOverviewCollection(ICollection<PriceChanges> objects)
        {
            throw new NotImplementedException();
        }

        public PriceChangesDto FromObjecttoDTO(PriceChanges obj)
        {
            return new PriceChangesDto
            {
                Id = obj.Id,
                PriceChange = obj.PriceChange,
                Type = obj.Type
            };
        
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

