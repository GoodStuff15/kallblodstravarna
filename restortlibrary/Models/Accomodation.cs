using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Models
{
    public class Accomodation
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MaxOccupancy { get; set; }
        [Required]
        public int AccomodationTypeId { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Accessibility> Accessibilities { get; set; } = new List<Accessibility>();

    }
}
