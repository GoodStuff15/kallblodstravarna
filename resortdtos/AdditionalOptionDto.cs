namespace resortdtos
{
    public class AdditionalOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool PerGuest { get; set; }
        public bool PerNight { get; set; }
    }

}
