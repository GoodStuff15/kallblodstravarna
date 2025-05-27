using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Models
{
    public class Accessibility
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Accomodation> Accomodations { get; set; } = new List<Accomodation>();

    }
}
