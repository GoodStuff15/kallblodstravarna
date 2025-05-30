using resortlibrary.Builders;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class CustomerBuilderTests
{
    private CustomerBuilder _builder;

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
    [DataTestMethod]
    [DataRow("valid.email@example.com")]
    [DataRow("namn.efternamn@mail.se")]
    public void AddEmail_ValidEmailAddress_ShouldSetEmail(string validEmail)
    {
        var customer = _builder.AddEmail(validEmail).Build();
        Assert.AreEqual(validEmail, customer.Email);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("incorrect-email")]
    [DataRow("email@.com")]
    [DataRow("email@mail")]
    public void AddEmail_InvalidEmailAddress_ShouldThrow(string invalidEmail)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
            _builder.AddEmail(invalidEmail));

        StringAssert.Contains(ex.Message.ToLower(), "email");
    }

    [DataTestMethod]
    [DataRow("0700000000")]
    [DataRow("(070) 700 00 00")]
    [DataRow("070-700-00-00")]
    [DataRow("070 700 00 00")]
    public void AddPhone_ValidPhoneNumber_ShouldSetPhone(string validPhone)
    {
        var customer = _builder.AddPhone(validPhone).Build();
        Assert.AreEqual(validPhone, customer.Phone);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("123")]
    [DataRow("abc")]
    [DataRow("070_700_0000")]
    public void AddPhone_InvalidPhoneNumber_ShouldThrow(string invalidPhone)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _builder.AddPhone(invalidPhone));
        StringAssert.Contains(ex.Message, "phone");
    }
}
    
    
    

