using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortdtos
{
    public class ModifyBookingDto
    {
        public int BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AccomodationId { get; set; }
        public ICollection<GuestDto> Guests { get; set; } = new List<GuestDto>(); 
        public ICollection<int> AdditionalOptionIds { get; set; } = new List<int>();
    }
}
