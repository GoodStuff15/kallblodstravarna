﻿using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace resortlibrary.Factories
{
    public class CustomerFactory : ICustomerFactory
    {
        public Customer CreateCustomer(string type, string firstName, string lastName, string email, string phone, string paymentMethod)
        {
            if(string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) || 
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(paymentMethod) || string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Fyll i samtliga fält");
            } 
            return new Customer
            {
                Type = type,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                PaymentMethod = paymentMethod,
                Bookings = new List<Booking>()
            };
        }
    }
}
