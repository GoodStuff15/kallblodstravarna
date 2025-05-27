using System.ComponentModel.DataAnnotations;

namespace resortdtos
{
    public class AvailableRoomRequestExclGuests
    {
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }

    }
}
