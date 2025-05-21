using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Models.DTOs
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
