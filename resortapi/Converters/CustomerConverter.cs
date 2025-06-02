using resortdtos;
using resortlibrary.Builders;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class CustomerConverter : IConverter<Customer, CreateCustomerRequestDTO>
    {
        private readonly CustomerBuilder _builder;

        public CustomerConverter(CustomerBuilder builder)
        {
            _builder = builder;
        }

        public CustomerDto FromObjectToCustomerDTO(Customer entity)
        {
            return new CustomerDto
            {
                Id = entity.Id,
                Type = entity.Type,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.Phone,
                PaymentMethod = entity.PaymentMethod
            };
        }

        public Customer FromDTOtoObject(CreateCustomerRequestDTO entity)
        {
            var customer=   _builder
                            .AddType(entity.Type)
                            .AddFirstName(entity.FirstName)
                            .AddLastName(entity.LastName)
                            .AddPhone(entity.PhoneNumber)
                            .AddEmail(entity.Email)
                            .AddPaymentMethod(entity.PaymentMethod)
                            .Build();

            return customer;
        }

        public ICollection<Customer> FromDTOtoObject_Collection(ICollection<CreateCustomerRequestDTO> collection)
        {
            throw new NotImplementedException();
        }

        public CreateCustomerRequestDTO FromObjecttoDTO(Customer entity)
        {
            return new CreateCustomerRequestDTO()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.Phone,
                PaymentMethod = entity.PaymentMethod
            };
        }

        public ICollection<CreateCustomerRequestDTO> FromObjecttoDTO_Collection(ICollection<Customer> collection)
        {
            throw new NotImplementedException();
        }
    }
}
