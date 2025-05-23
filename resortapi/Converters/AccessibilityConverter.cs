using resortdtos;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class AccessibilityConverter : IConverter<Accessibility, AccessibilityDto>
    {
        public Accessibility FromDTOtoObject(AccessibilityDto dto)
        {
            var obj = new Accessibility();
            obj.Name = dto.Name;
            obj.Description = dto.Description;

            return obj;
        }

        public AccessibilityDto FromObjecttoDTO(Accessibility obj)
        {
            var dto = new AccessibilityDto();
            dto.Name = obj.Name;
            dto.Description = obj.Description;

            return dto;
        }
    }
}
