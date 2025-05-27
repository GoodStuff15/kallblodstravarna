using resortdtos;
using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class CustomerConverter : IConverter<Customer, CreateCustomerRequestDTO>
    {
        private readonly ICustomerBuilder _builder;

        public CustomerConverter(ICustomerBuilder builder)
        {
            _builder = builder;
        }

        public Customer FromDTOtoObject(CreateCustomerRequestDTO entity)
        {
            return _builder
                .AddType(entity.Type)
                .AddFirstName(entity.FirstName)
                .AddLastName(entity.LastName)
                .AddPhone(entity.PhoneNumber)
                .AddEmail(entity.Email)
                .AddPaymentMethod(entity.PaymentMethod)
                .Build();
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
