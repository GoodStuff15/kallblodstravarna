using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
