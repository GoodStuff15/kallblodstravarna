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
        Assert.AreEqual("Johan", customer.FirstName);
        Assert.AreEqual("Hansson", customer.LastName);
        Assert.AreEqual("hasse@hotmail.com", customer.Email);
        Assert.AreEqual("0700000000", customer.Phone);
        Assert.AreEqual("Cash", customer.PaymentMethod);
    }
    
    
}
