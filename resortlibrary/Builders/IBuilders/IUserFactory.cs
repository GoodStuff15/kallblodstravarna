using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortlibrary.Factories.IFactories
{
    public interface IUserFactory
    {
        User CreateUser(string username, string passwordHash, string role, int customerId, Customer? customer = null, string? refreshToken = null, DateTime? refreshTokenExpiryTime = null);
    }
}
