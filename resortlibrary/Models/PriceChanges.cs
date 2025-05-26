using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Models
{
    public class PriceChanges
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public float PriceChange { get; set; }
        public string Type { get; set; } = string.Empty;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
