
using resortlibrary.Models;
using resortdtos;
namespace resortapi.Converters

{
public interface ICustomerConverter : IConverter<Customer, CreateCustomerRequestDTO>
    {
        public CustomerDto FromObjectToCustomerDto(Customer customer);
    }

}

