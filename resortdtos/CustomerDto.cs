namespace resortdtos
{
    public record CustomerDto
    {
        public int Id { get; init; }
        public string Type { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string PaymentMethod { get; init; }
    }
}