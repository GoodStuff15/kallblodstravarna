using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
        public DateTime TimeOfBooking { get; set; }
        public bool Active { get; set; }
        public DateTime? CancellationDate { get; set; }
        public decimal Cost { get; set; }
        public decimal AmountPaid { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int AccomodationId { get; set; }
        public Accomodation Accomodation { get; set; }
        public ICollection<Guest> Guests { get; set; } = new List<Guest>();
        public ICollection<PriceChanges> PriceChanges { get; set; } = new List<PriceChanges>();
        public ICollection<AdditionalOption> AdditionalOptions { get; set; } = new List<AdditionalOption>();

    }
}
