using System.ComponentModel.DataAnnotations;

namespace resortdtos
{
    public class GuestDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        public bool IsChild { get; set; }
    }
}
