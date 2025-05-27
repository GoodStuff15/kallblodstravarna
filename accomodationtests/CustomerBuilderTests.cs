using resortlibrary.Builders;
using resortlibrary.Builders.IBuilders;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class CustomerBuilderTests
{
    private ICustomerBuilder _builder;

    [TestInitialize]
    public void Setup()
    {
        _builder = new CustomerBuilder();
    }
    [TestMethod]
    public void CreateCustomer_WithValidData_ReturnsCustomer()
    {
        string type = "Premium";
        string firstName = "Johan";
        string lastName = "Hansson";
        string email = "hasse@hotmail.com";
        string phone = "0707000000";
        string paymentMethod = "Cash";

        var customer = _builder.AddFirstName(firstName)
            .AddLastName(lastName)
            .AddType(type)
            .AddEmail(email)
            .AddPhone(phone)
            .AddPaymentMethod(paymentMethod)
            .Build();

        Assert.IsNotNull(customer);
        Assert.AreEqual(type, customer.Type);
        Assert.AreEqual(firstName, customer.FirstName);
        Assert.AreEqual(lastName, customer.LastName);
        Assert.AreEqual(email, customer.Email);
        Assert.AreEqual(phone, customer.Phone);
        Assert.AreEqual(paymentMethod, customer.PaymentMethod);
        Assert.IsNotNull(customer.Bookings);
        Assert.AreEqual(0, customer.Bookings.Count);
    }

    [DataTestMethod]
    [DataRow(null, "Johan", "Hansson", "hasse@hotmail.com", "0707112233", "Cash")]
    [DataRow("", "Johan", "Hansson", "hasse@hotmail.com", "0707112233", "Cash")]
    [DataRow(" ", "Johan", "Hansson", "hasse@hotmail.com", "0707112233", "Cash")]
    [DataRow("Premium", "", "Hansson", "hasse@hotmail.com", "0707112233", "Cash")]
    [DataRow("Premium", " ", "Hansson", "hasse@hotmail.com", "0707112233", "Cash")]
    [DataRow("Premium", "Johan", null, "hasse@hotmail.com", "0707112233", "Cash")]
    [DataRow("Premium", "Johan", " ", "hasse@hotmail.com", "0707112233", "Cash")]
    [DataRow("Premium", "Johan", "Hansson", "", "0707112233", "Cash")]
    [DataRow("Premium", "Johan", "Hansson", " ", "0707112233", "Cash")]
    [DataRow("Premium", "Johan", "Hansson", "hasse@hotmail.com", null, "Cash")]
    [DataRow("Premium", "Johan", "Hansson", "hasse@hotmail.com", " ", "Cash")]
    [DataRow("Premium", "Johan", "Hansson", "hasse@hotmail.com", "0707112233", "")]
    [DataRow("Premium", "Johan", "Hansson", "hasse@hotmail.com", "0707112233", " ")]
    public void CreateCustomer_InvalidInputs_ShouldThrowException(string type, string firstName, string lastName, string email, string phone, string paymentMethod)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
         _builder.AddFirstName(firstName)
            .AddLastName(lastName)
            .AddType(type)
            .AddEmail(email)
            .AddPhone(phone)
            .AddPaymentMethod(paymentMethod)
            .Build()
        );
    }
}
    
    
    

