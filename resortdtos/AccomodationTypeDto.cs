namespace resortdtos
{
    public class AccomodationTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
    }
}
