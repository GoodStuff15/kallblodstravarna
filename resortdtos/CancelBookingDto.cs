using System.ComponentModel.DataAnnotations;

namespace resortdtos
{
    public class CancelBookingDto
    {
        [Required]
        public int BookingId { get; set; }
        public bool Active { get; set; }
       
    }
}
