using resortlibrary.Factories;
using resortlibrary.Factories.IFactories;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class UserFactoryTests
{
    private IUserBuilder _factory;

    [TestInitialize]
    public void Setup()
    {
        _factory = new UserBuilder();
    }
    [TestMethod]
    public void CreateUser_ValidData_ShouldReturnUser()
    {
        var customer = new Customer { 
            Id = 1, 
            FirstName = "Johan",
            LastName = "Hansson",
            Email = "hasse@hotmail.com",
            Phone = "0707000000",
            PaymentMethod = "Cash",
            Type = "Premium"        
        };
        var expiry = DateTime.UtcNow.AddDays(5);

        var user = _factory.CreateUser("johan", "pass123", "Guest", 1, customer, "token", expiry);

        Assert.IsNotNull(user);
        Assert.AreEqual("johan", user.Username);
        Assert.AreEqual("pass123", user.PasswordHash);
        Assert.AreEqual("Guest", user.Role);
        Assert.AreEqual(1, user.CustomerId);
        Assert.AreEqual(customer, user.Customer);
        Assert.AreEqual("token", user.RefreshToken);
        Assert.AreEqual(expiry, user.RefreshTokenExpiryTime);
    }

    [DataTestMethod]
    [DataRow(null, "password", "User", 1)]
    [DataRow("", "password", "User", 1)]
    [DataRow(" ", "password", "User", 1)]
    [DataRow("user", null, "User", 1)]
    [DataRow("user", "", "User", 1)]
    [DataRow("user", " ", "User", 1)]
    [DataRow("user", "password", null, 1)]
    [DataRow("user", "password", "", 1)]
    [DataRow("user", "password", " ", 1)]
    [DataRow("user", "password", "User", 0)]
    [DataRow("user", "password", "User", -5)]
    public void CreateUser_InvalidData_ShouldThrow(string username, string passwordHash, string role, int customerId)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
        _factory.CreateUser(username, passwordHash, role, customerId)
        );
        Assert.AreEqual("Fyll i samtliga fält", ex.Message);
    }
    
}
