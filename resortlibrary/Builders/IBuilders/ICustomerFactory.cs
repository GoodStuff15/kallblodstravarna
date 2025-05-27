using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface ICustomerFactory
    {
        Customer CreateCustomer(string type, string firstName, string lastName, string email, string phone, string paymentMethod);
    }
}
