using resortlibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Invalid email address");

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
                throw new ArgumentException("Invalid email address");

            _customer.Email = email;
            return this;
        }

        public CustomerBuilder AddPhone(string phone)
        {
            var pattern = @"^(\+46|0)?\s*(\(?\d{2,4}\)?)[\s-]*\d{2,3}[\s-]*\d{2}[\s-]*\d{2}$";
            var regex = new Regex(pattern);

            if (string.IsNullOrWhiteSpace(phone) || !regex.IsMatch(phone))
            {
                throw new ArgumentException("Invalid phone number.");
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
