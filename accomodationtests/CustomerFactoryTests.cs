using restortlibrary.Factories;

namespace accomodationtests;

[TestClass]
public class CustomerFactoryTests
{
    private CustomerFactory _factory;

    [TestInitialize]
    public void Setup() => _factory = new CustomerFactory();

    [TestMethod]
    public void CreateCustomer_WithValidData_ReturnsCustomer()
    {
        var customer = _factory.CreateCustomer("Premium", "Johan", "Hansson", "hasse@hotmail.com", "0700000000", "Cash");

        Assert.IsNotNull(customer);
        Assert.AreEqual("Premium", customer.Type);
        Assert.AreEqual("Johan", customer.FirstName);
        Assert.AreEqual("Hansson", customer.LastName);
        Assert.AreEqual("hasse@hotmail.com", customer.Email);
        Assert.AreEqual("0700000000", customer.Phone);
        Assert.AreEqual("Cash", customer.PaymentMethod);
    }
    [TestMethod]
    [DataRow(null, "Johan", "Hansson", "hasse@hotmail.com", "0700000000", "Cash")]
    [DataRow("Premium", "", "Hansson", "hasse@hotmail.com", "0700000000", "Cash")]
    [DataRow("Premium", "Johan", "", "hasse@hotmail.com", "0700000000", "Cash")]
    [DataRow("Premium", "Johan", "Hansson", "", "0700000000", "Cash")]
    [DataRow("Premium", "Johan", "Hansson", "hasse@hotmail.com", null, "Cash")]
    [DataRow("Premium", "Johan", "Hansson", "hasse@hotmail.com", "0700000000", "")]
    public void CreateCustomer_InvalidInput_Throws(
        string type,
        string firstName,
        string lastName,
        string email,
        string phone,
        string paymentMethod)
    {
        Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateCustomer(type, firstName, lastName, email, phone, paymentMethod));
    }
    
    
}
