using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class PriceChangesConverter : IConverter<PriceChanges, PriceChangesDto>
    {
        public PriceChanges FromDTOtoObject(PriceChangesDto dto)
        {
            throw new NotImplementedException();
        }

        public ICollection<PriceChanges> FromDTOtoObject_Collection(ICollection<PriceChangesDto> collection)
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
            var result = new List<PriceChangesDto>();

            foreach (var item in collection)
            {
                result.Add(new PriceChangesDto
                {
                    Id = item.Id,
                    PriceChange = item.PriceChange,
                    Type = item.Type
                });
            }

            return result;
        }
    }
}
