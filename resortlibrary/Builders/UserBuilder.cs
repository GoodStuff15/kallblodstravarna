
using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortlibrary.Builders
{
    public class UserBuilder
    {
        private User _user;

        public UserBuilder()
        {
            _user = new User();
        }
        public UserBuilder WithUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Användarnamn måste anges.");
            }
            _user.Username = username;
            return this;
        }
        public UserBuilder WithPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Lösenord måste anges.");                
            }
            _user.PasswordHash = passwordHash;
            return this;
        }
        public UserBuilder WithRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Roll måste anges.");
            }    
            _user.Role = role;
            return this;
        }

        public UserBuilder WithCustomer(Customer customer)
        {
            _user.Customer = customer;
            return this;
        }

        public UserBuilder WithRefreshToken(string refreshToken)
        {
            _user.RefreshToken = refreshToken;
            return this;
        }

        public UserBuilder WithRefreshTokenExpiry(DateTime expiryTime)
        {
            _user.RefreshTokenExpiryTime = expiryTime;
            return this;
        }
        public User Build()
        {
            if (string.IsNullOrWhiteSpace(_user.Username))
            {
                throw new InvalidOperationException("Användarnamn har inte angetts.");
            }
                
            if (string.IsNullOrWhiteSpace(_user.PasswordHash))
            {
                throw new InvalidOperationException("Lösenord har inte angetts.");
            }
                
            if (string.IsNullOrWhiteSpace(_user.Role))
            {
                throw new InvalidOperationException("Roll har inte angetts.");
            }
            return _user;
        }
    }
}
