using resortlibrary.Builders.IBuilders;
using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortlibrary.Builders
{
    public class UserBuilder : IUserBuilder
    {
        private string? _username;
        private string? _passwordHash;
        private string? _role;
        private Customer? _customer;
        private string? _refreshToken;

        private DateTime? _refreshTokenExpiryTime;

        public UserBuilder WithUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Användarnamn måste anges.", nameof(username));

            _username = username;
            return this;
        }
        public UserBuilder WithPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Lösenord måste anges.", nameof(passwordHash));
            }
                
            _passwordHash = passwordHash;
            return this;
        }
        public UserBuilder WithRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Roll måste anges.", nameof(role));
            _role = role;
            return this;
        }

        public UserBuilder WithCustomer(Customer customer)
        {
            _customer = customer;
            return this;
        }

        public UserBuilder WithRefreshToken(string refreshToken)
        {
            _refreshToken = refreshToken;
            return this;
        }

        public UserBuilder WithRefreshTokenExpiry(DateTime expiryTime)
        {
            _refreshTokenExpiryTime = expiryTime;
            return this;
        }
        public User Build()
        {
            if (_username == null)
                throw new InvalidOperationException("Användarnamn har inte angetts.");
            if (_passwordHash == null)
                throw new InvalidOperationException("Lösenord har inte angetts.");
            if (_role == null)
                throw new InvalidOperationException("Roll har inte angetts.");

            return new User
            {
                Username = _username,
                PasswordHash = _passwordHash,
                Role = _role,
                Customer = _customer,
                RefreshToken = _refreshToken,
                RefreshTokenExpiryTime = _refreshTokenExpiryTime
            };
        }
    }
}
