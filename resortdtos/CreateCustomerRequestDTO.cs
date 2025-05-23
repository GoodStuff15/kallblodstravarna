﻿namespace resortdtos
{
    public record CreateCustomerRequestDTO
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }

        public string PhoneNumber { get; init; }

        public string PaymentMethod { get; init; }
    }
}
