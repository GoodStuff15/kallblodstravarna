using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Models
{
    public class AdditionalOption
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
