using restortlibrary.Factories.IFactories;
using restortlibrary.Models;
using restortlibrary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Converters
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

    }
}
