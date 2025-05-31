namespace resortdtos
{
    public class AccomodationDto
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        public int MaxOccupancy { get; set; }
        public int AccomodationTypeId { get; set; }
        public ICollection<int> AccessibilityIds { get; set; } = new List<int>();
    }
}
