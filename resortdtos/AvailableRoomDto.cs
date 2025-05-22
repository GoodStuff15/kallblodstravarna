namespace resortdtos
{
    public class AvailableRoomDto
    {
        public int Id { get; set; }
        public string AccomodationType { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxOccupancy { get; set; }
        public decimal BasePrice { get; set; }
        public ICollection<AccessibilityDto> Accessibility { get; set; } = new List<AccessibilityDto>();

    }
}
