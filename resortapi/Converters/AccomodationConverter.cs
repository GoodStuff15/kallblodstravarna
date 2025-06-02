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

        public ICollection<AccomodationDto> FromObjecttoDTO_Collection(ICollection<Accomodation> collection)
        {
            return collection.Select(a => FromObjecttoDTO(a)).ToList();
        }
    }
}
