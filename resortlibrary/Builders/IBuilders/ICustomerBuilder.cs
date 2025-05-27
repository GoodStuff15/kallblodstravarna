using resortlibrary.Models;

namespace resortlibrary.Builders.IBuilders
{
    public interface ICustomerBuilder
    {
        CustomerBuilder AddType(string type);
        CustomerBuilder AddFirstName(string firstName);
        CustomerBuilder AddLastName(string lastName);
        CustomerBuilder AddEmail(string email);
        CustomerBuilder AddPhone(string phone);
        CustomerBuilder AddPaymentMethod(string paymentMethod);
        Customer Build();
    }
}
