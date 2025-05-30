using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public interface ICustomerService
    {

        public bool ValidateCustomer(Customer customer);

        public Customer ConvertToCustomer(CreateCustomerRequestDTO dto);
    
        public bool CreateCustomer(CreateCustomerRequestDTO dto);
    }
}
