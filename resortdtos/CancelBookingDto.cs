using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortdtos
{
    public class CancelBookingDto
    {
        [Required]
        public int BookingId { get; set; }
        public bool Active { get; set; }
       
    }
}
