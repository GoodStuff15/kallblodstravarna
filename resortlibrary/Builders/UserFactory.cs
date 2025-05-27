using resortlibrary.Factories.IFactories;
using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortlibrary.Factories
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string username, string passwordHash, string role, int customerId, Customer? customer = null, string? refreshToken = null, DateTime? refreshTokenExpiryTime = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Användarnamn måste anges.");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Lösenord måste anges.");
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Roll måste anges.");

            return new User
            {
                Username = username,
                PasswordHash = passwordHash,
                Role = role,
                CustomerId = customerId,
                Customer = customer,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = refreshTokenExpiryTime
            };
        }
        
        
    }
}
