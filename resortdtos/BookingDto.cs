using System.ComponentModel.DataAnnotations;

namespace resortdtos
{
    public class BookingDto
    {
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
        [Required]
        public int AccomodationId { get; set; }
        public int CustomerId { get; set; }
        public ICollection<GuestDto> Guests { get; set; } = new List<GuestDto>(); // list of guests for the room
        public ICollection<int> AdditionalOptionIds { get; set; } = new List<int>();  // more than one additional option for the future

    }
}
