using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories.IFactories
{
    public interface ICustomerFactory
    {
        Customer CreateCustomer(string type, string firstName, string lastName, string email, string phone, string paymentMethod);
    }
}
