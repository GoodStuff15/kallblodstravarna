using resortlibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Builders
{
    public class CustomerBuilder
    {
        public Customer _customer;

        public CustomerBuilder AddType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Fyll i kundtyp");

            _customer.Type = type;
            return this;
        }

        public CustomerBuilder AddFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Fyll i kundtyp");

            _customer.FirstName = firstName;
            return this;
        }

        public CustomerBuilder AddLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Fyll i kundtyp");

            _customer.LastName = lastName;
            return this;
        }

        public CustomerBuilder AddEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Fyll i kundtyp");

            _customer.Email = email;
            return this;
        }

        public CustomerBuilder AddPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Fyll i kundtyp");

            _customer.Phone = phone;
            return this;
        }

        public CustomerBuilder AddPaymentMethod(string paymentMethod)
        {
            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new ArgumentException("Fyll i kundtyp");

            _customer.PaymentMethod = paymentMethod;
            return this;
        }

        public Customer Build()
        {
            return _customer;
        }
    }
}
