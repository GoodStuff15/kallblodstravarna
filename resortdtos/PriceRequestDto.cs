namespace resortdtos
{
    public record PriceRequestDto
    {
        // Minimum 1 night
        public int Duration { get; init; } = 1;
        public int AccomodationId { get; init; }
        public int? CustomerId { get; init; }
        public int GuestCount { get; init; }
        public ICollection<int> AdditonalOptionIds { get; init; } = new List<int>();

    }
}
