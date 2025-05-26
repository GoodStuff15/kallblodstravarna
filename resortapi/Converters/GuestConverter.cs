using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class GuestConverter : IConverter<Guest, GuestDto>
    {
        public Guest FromDTOtoObject(GuestDto dto)
        {
            var obj = new Guest()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age
            };

            return obj;
        }

        public ICollection<Guest> FromDTOtoObject_Collection(ICollection<GuestDto> collection)
        {
            var objCollection = new List<Guest>();

            foreach (var dto in collection)
            {
                var obj = new Guest()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age
                };
                objCollection.Add(obj);
            }

            return objCollection;
        }

        public GuestDto FromObjecttoDTO(Guest obj)
        {
            var dto = new GuestDto()
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Age = obj.Age
            };

            return dto;
        }

        public ICollection<GuestDto> FromObjecttoDTO_Collection(ICollection<Guest> collection)
        {
            var objCollection = new List<GuestDto>();

            foreach (var obj in collection)
            {
                var dto = new GuestDto()
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    Age = obj.Age
                };
                objCollection.Add(dto);
            }

            return objCollection;
        }
    }
}
