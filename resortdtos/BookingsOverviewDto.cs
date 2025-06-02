namespace resortdtos
{
    public class BookingsOverviewDto
    {
        public int BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AccomodationId { get; set; }  
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public bool Active { get; set; } = true;
        public bool Cost { get; set; }
    }
}
