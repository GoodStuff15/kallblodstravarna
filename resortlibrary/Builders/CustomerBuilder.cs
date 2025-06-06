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

        public CustomerBuilder AddType(string type)//Set customer type. Cant be empty or whitespace
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Enter customer type.");
            }

            _customer.Type = type;
            return this;
        }

        public CustomerBuilder AddFirstName(string firstName)//Set customer first name. Cant be empty or whitespace
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Enter first name.");
            }

            _customer.FirstName = firstName;
            return this;
        }

        public CustomerBuilder AddLastName(string lastName)//Set customer last name. Cant be empty or whitespace
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Enter last name.");
            }

            _customer.LastName = lastName;
            return this;
        }

        public CustomerBuilder AddEmail(string email)//Set customer email. Cant be empty or whitespace
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Invalid email address");
            }

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
            {
                throw new ArgumentException("Invalid email address");
            }

            _customer.Email = email;
            return this;
        }

        public CustomerBuilder AddPhone(string phone)//Set customer phone. Must match swedish phone number pattern
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

        public CustomerBuilder AddPaymentMethod(string paymentMethod)//Set customer payment method. Cant be empty or whitespace
        {
            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new ArgumentException("Enter payment method.");
            }

            _customer.PaymentMethod = paymentMethod;
            return this;
        }

        public CustomerBuilder AddBookings(ICollection<Booking> bookings)//Set customer bookings. Must contain at leaste one booking.
        {
            if (bookings == null || bookings.Count == 0)
            {
                throw new ArgumentException("Customer must have min one reservation.");
            }

            _customer.Bookings = bookings;
            return this;
        }

        public Customer Build()//Build customer-object
        {
            return _customer;
        }
    }
}
