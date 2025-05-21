using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortdtos
{
    public class BookingsOverviewDto
    {
        public int BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AccomodationId { get; set; }  
        public int CustomerId { get; set; }
        public bool Active { get; set; } = true;
    }
}
