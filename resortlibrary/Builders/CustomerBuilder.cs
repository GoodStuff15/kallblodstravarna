using resortlibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace resortlibrary.Builders
{
    public class CustomerBuilder
    {
        private Customer _customer;

        public CustomerBuilder()
        {
            _customer = new Customer
            {
                Bookings = new List<Booking>()
            };
        }

        public CustomerBuilder AddType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Fyll i kundtyp");
            }

            _customer.Type = type;
            return this;
        }

        public CustomerBuilder AddFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Fyll i förnamn");
            }

            _customer.FirstName = firstName;
            return this;
        }

        public CustomerBuilder AddLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Fyll i efternamn");
            }

            _customer.LastName = lastName;
            return this;
        }

        public CustomerBuilder AddEmail(string email)
        {
            var emailValidator = new EmailAddressAttribute();
            if (string.IsNullOrWhiteSpace(email) || !emailValidator.IsValid(email))
            {
                throw new ArgumentException("Invalid email address");
            }

            _customer.Email = email;
            return this;
        }

        public CustomerBuilder AddPhone(string phone)
        {
            var phoneValidator = new PhoneAttribute();
            if (string.IsNullOrWhiteSpace(phone) || !phoneValidator.IsValid(phone))
            {
                throw new ArgumentException("Invalid phone number");
            }

            _customer.Phone = phone;
            return this;
        }

        public CustomerBuilder AddPaymentMethod(string paymentMethod)
        {
            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new ArgumentException("Fyll i betalningsmetod");
            }

            _customer.PaymentMethod = paymentMethod;
            return this;
        }

        public CustomerBuilder AddBookings(ICollection<Booking> bookings)
        {
            if (bookings == null || bookings.Count == 0)
            {
                throw new ArgumentException("Kund måste ha minst en bokning.");
            }

            _customer.Bookings = bookings;
            return this;
        }

        public Customer Build()
        {
            return _customer;
        }
    }
}
