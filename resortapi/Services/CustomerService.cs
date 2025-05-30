using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class CustomerService
    {
        private IRepository<Customer> _repo;
        private IConverter<Customer, CreateCustomerRequestDTO> _converter;

        public CustomerService(IRepository<Customer> repo)
        {
            _repo = repo;
        }

        public bool ValidateCustomer(Customer customer)
        {
            // CustomerValidator(customer) innehåller allt det här? 
            if(customer.FirstName.Length < 2)
            {
                throw new ArgumentException("To few letters in first name");
            }
            if (customer.LastName.Length < 2)
            {
                throw new ArgumentException("To few letters in first name");
            }
            if (!customer.Email.Contains("@"))
            {
                throw new ArgumentException("Invalid email adress");
            }
            

            // etc.

            return true;
        }

        public Customer ConvertToCustomer(CreateCustomerRequestDTO requestDTO)
        {
            return _converter.FromDTOtoObject(requestDTO);
        }

        public bool CreateCustomer(CreateCustomerRequestDTO requestDTO)
        {
            // Conversion
            var customer = ConvertToCustomer(requestDTO);

            // Validation
            if(ValidateCustomer(customer))
            {
                //Database action
                _repo.CreateAsync(customer);
            
                return true;
            }
            else
            {
                return false;
            }

        }

         
    }
}
