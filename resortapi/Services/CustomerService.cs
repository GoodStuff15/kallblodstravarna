using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class CustomerService : ICustomerService
    {
        private IRepository<Customer> _repo;
        private ICustomerConverter _converter;

        public CustomerService(IRepository<Customer> repo, ICustomerConverter converter)
        {
            _repo = repo;
            _converter = converter;
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

        public CustomerDto ConvertToCustomerDto(Customer customer)
        {
            return _converter.FromObjectToCustomerDto(customer);
        }

        public async Task<CustomerDto> CreateCustomer(CreateCustomerRequestDTO requestDTO)
        {
            // Conversion
            var customer = ConvertToCustomer(requestDTO);

            // Validation
            if(ValidateCustomer(customer))
            {
                //Database action
                _repo.CreateAsync(customer);

                return ConvertToCustomerDto(customer) ;
            }
            else
            {
                throw new Exception("Customer could not be created");
            }

        }

        
        public async Task<CustomerDto> GetCustomer(int id)
        {
            // From db
            var customer = await _repo.GetAsync(id);
            // Validate
            if(ValidateCustomer(customer))
            {
                return ConvertToCustomerDto(customer) ;
            }
            else
            {
                throw new Exception("Could not get customer");
            }
        }

         
    }
}
