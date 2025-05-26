using resortdtos;
using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortapi.Converters
{
    public class CustomerConverter : IConverter<Customer, CreateCustomerRequestDTO>
    {
        private readonly ICustomerFactory _factory;

        public CustomerConverter(ICustomerFactory factory)
        {
            _factory = factory;
        }

        public Customer FromDTOtoObject(CreateCustomerRequestDTO entity)
        {
            return _factory.CreateCustomer("Basic", entity.FirstName, entity.LastName, entity.Email, entity.PhoneNumber, entity.PaymentMethod);

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
