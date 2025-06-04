using resortdtos;
using resortlibrary.Builders;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class GuestConverter : IConverter<Guest, GuestDto>
    {
        public Guest FromDTOtoObject(GuestDto dto)
        {
            var obj = new GuestBuilder()
                      .AddFirstName(dto.FirstName)
                      .AddLastName(dto.LastName)
                      .AddAge(dto.Age)
                      .Build();

            return obj;
        }

        public ICollection<Guest> FromDTOtoObject_Collection(ICollection<GuestDto> collection)
        {
            var objCollection = new List<Guest>();

            foreach (var dto in collection)
            {
                var obj = new GuestBuilder()
                      .AddFirstName(dto.FirstName)
                      .AddLastName(dto.LastName)
                      .AddAge(dto.Age)
                      .Build();

                objCollection.Add(obj);
            }

            return objCollection;
        }

        public ICollection<GuestDto> FromObjectCollection_ToOverviewCollection(ICollection<Guest> objects)
        {
            throw new NotImplementedException();
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
