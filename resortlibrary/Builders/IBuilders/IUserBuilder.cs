using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortlibrary.Builders.IBuilders
{
    public interface IUserBuilder
    {
        UserBuilder WithUsername(string username);
        UserBuilder WithPasswordHash(string passwordHash);
        UserBuilder WithRole(string role);
        UserBuilder WithCustomerId(int customerId);
        UserBuilder WithCustomer(Customer customer);
        UserBuilder WithRefreshToken(string refreshToken);
        UserBuilder WithRefreshTokenExpiry(DateTime expiryTime);
        User Build();
    }
}
