using System.ComponentModel.DataAnnotations;

namespace resortdtos
{
    public class AvailableRoomRequest
    {
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }

    }
}
