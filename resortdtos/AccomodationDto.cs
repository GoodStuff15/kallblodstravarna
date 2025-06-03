namespace resortdtos
{
    public class AccomodationDto
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public int MaxOccupancy { get; set; }
        public int AccomodationTypeId { get; set; }
        public string? AccomodationTypeName { get; set; } // valfritt, för att visa typnamn

        // Om du vill kan du även inkludera accessibilities som en lista med strängar eller egna DTOs
        public List<string>? Accessibilities { get; set; }
        public ICollection<int> AccessibilityIds { get; set; } = new List<int>();
    }

}
