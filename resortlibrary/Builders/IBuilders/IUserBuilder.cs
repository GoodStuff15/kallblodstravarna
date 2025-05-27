using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortlibrary.Factories.IFactories
{
    public interface IUserBuilder
    {
        IUserBuilder WithUsername(string username);
        IUserBuilder WithPasswordHash(string passwordHash);
        IUserBuilder WithRole(string role);
        IUserBuilder WithCustomerId(int customerId);
        IUserBuilder WithCustomer(Customer customer);
        IUserBuilder WithRefreshToken(string refreshToken);
        IUserBuilder WithRefreshTokenExpiry(DateTime expiryTime);
        User Build();
    }
}
