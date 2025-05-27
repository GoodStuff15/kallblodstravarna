using resortlibrary.Builders;
using resortlibrary.Models;

namespace accomodationtests;

[TestClass]
public class UserBuilderTests
{
    private UserBuilder _builder;

    [TestInitialize]
    public void Setup()
    {
        _builder = new UserBuilder();
    }
    [TestMethod]
    public void Build_WithValidData_ShouldReturnUser()
    {
        var customer = new Customer
        {
            Id = 1,
            FirstName = "Johan",
            LastName = "Hansson",
            Email = "hasse@hotmail.com",
            Phone = "0707000000",
            PaymentMethod = "Cash",
            Type = "Premium"
        };
        var expiry = DateTime.UtcNow.AddDays(5);

        var user = _builder
            .WithUsername("johan")
            .WithPasswordHash("pass123")
            .WithRole("Guest")
            .WithCustomer(customer)
            .WithRefreshToken("token")
            .WithRefreshTokenExpiry(expiry)
            .Build();

        Assert.IsNotNull(user);
        Assert.AreEqual("johan", user.Username);
        Assert.AreEqual("pass123", user.PasswordHash);
        Assert.AreEqual("Guest", user.Role);
        Assert.AreEqual(customer, user.Customer);
        Assert.AreEqual("token", user.RefreshToken);
        Assert.AreEqual(expiry, user.RefreshTokenExpiryTime);
    }

    [DataTestMethod]
    [DataRow(null, "password", "User")]
    [DataRow("", "password", "User")]
    [DataRow(" ", "password", "User")]
    [DataRow("user", null, "User")]
    [DataRow("user", "", "User")]
    [DataRow("user", " ", "User")]
    [DataRow("user", "password", null)]
    [DataRow("user", "password", "")]
    [DataRow("user", "password", " ")]
    public void Build_MissingRequiredFields_ShouldThrowInvalidOperationException(string username, string passwordHash, string role)
    {
        if (!string.IsNullOrWhiteSpace(username))
        {
            _builder.WithUsername(username);
        }
            
        if (!string.IsNullOrWhiteSpace(passwordHash))
        {
            _builder.WithPasswordHash(passwordHash);
        }
            
        if (!string.IsNullOrWhiteSpace(role))
        {
            _builder.WithRole(role);
        }
            
        Assert.ThrowsException<InvalidOperationException>(() => _builder.Build());
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void WithUsername_InvalidValues_ShouldThrowArgumentException(string username)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() => _builder.WithUsername(username));
        Assert.AreEqual("Användarnamn måste anges.", ex.Message);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void WithPasswordHash_InvalidValues_ShouldThrowArgumentException(string passwordHash)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() => _builder.WithPasswordHash(passwordHash));
        Assert.AreEqual("Lösenord måste anges.", ex.Message);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void WithRole_InvalidValues_ShouldThrowArgumentException(string role)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() => _builder.WithRole(role));
        Assert.AreEqual("Roll måste anges.", ex.Message);
    }
    
}
