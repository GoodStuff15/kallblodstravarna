using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortdtos
{
    public class BookingDetailsDto
    {
        public int BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime? TimeOfBooking { get; set; }
        public bool Active { get; set; }
        public bool Cancelled { get; set; }
        public DateTime? CancellationDate { get; set; }
        public decimal Cost { get; set; }
        public decimal AmountPaid { get; set; }

        public CustomerDto Customer { get; set; }
        public AccomodationDto Accomodation { get; set; }
        public ICollection<GuestDto> Guests { get; set; }
        public ICollection<AdditionalOptionDto> AdditionalOptions { get; set; }
        public ICollection<PriceChangesDto> PriceChanges { get; set; }
    }

}
