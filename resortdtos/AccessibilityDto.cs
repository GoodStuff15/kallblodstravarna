using System.ComponentModel.DataAnnotations;

namespace resortdtos
{
    public class AccessibilityDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
