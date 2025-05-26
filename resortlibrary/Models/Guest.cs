using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

    }
}
