using System.ComponentModel.DataAnnotations;

namespace resortdtos
{
    public class AccessibilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
