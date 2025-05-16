using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using resortapi.Services;
using resortdtos;
using restortlibrary.Data;

namespace authtests;

[TestClass]
public class AuthTests
{
    private IAuthService _authService;
    private ResortContext _context;

    [TestInitialize]
    public void Setup()
    {
        var inMemorySettings = new Dictionary<string, string>
    {
        {"AppSettings:Token", "supersecretkey1234567890"},
        {"AppSettings:Issuer", "testissuer"},
        {"AppSettings:Audience", "testaudience"}
    };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddDbContext<ResortContext>(options =>
            options.UseInMemoryDatabase("TestDb"));

        services.AddScoped<IAuthService, AuthService>();

        var serviceProvider = services.BuildServiceProvider();

        _authService = serviceProvider.GetRequiredService<IAuthService>();
        _context = serviceProvider.GetRequiredService<ResortContext>();
    }

    [TestMethod]
    [DataRow("user1", "Password2@", null)]
    [DataRow("user2", "Password2@", "")]
    public async Task RegisterNewUser_InvalidRole_ShouldThrowArgumentException(string username, string password, string role)
    {
        // Given a new user with a unique username and password
        var userDto = new UserDto
        {
            Username = username,
            Password = password,
            Role = role
        };

        // When the user registers
        var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
        {
            await _authService.RegisterAsync(userDto);
        });

        //Then an ArgumentException should be thrown
        Assert.AreEqual("Role is required.", ex.Message);
    }

    [TestMethod]
    [DataRow("user3", "Password2@", "Admin")]
    [DataRow("user4", "TestPass3#", "User")]
    [DataRow("user5", "TestPass3#", "Receptionist")]
    public async Task RegisterNewUser_ShouldCreateUser(string username, string password, string role)
    {
        // Given a new user with a unique username and password
        var userDto = new UserDto
        {
            Username = username,
            Password = password,
            Role = role
        };

        // When the user registers
        var user = await _authService.RegisterAsync(userDto);

        // Then the user should be created successfully
        Assert.IsNotNull(user);
        Assert.AreEqual(username, user.Username);
    }

    [TestMethod]
    [DataRow("ab", "Password1@", false)]                            // Användarnamn för kort
    [DataRow("thisusernameiswaytoolong123", "Password1@", false)]   // Användarnamn för långt
    [DataRow("user$", "Password1@", false)]                         // Ogiltigt tecken i användarnamn
    [DataRow("validuser", "short", false)]                          // Lösenord för kort
    [DataRow("validuser", "alllowercase1@", false)]                 // Saknar stor bokstav
    [DataRow("validuser", "ALLUPPERCASE1@", false)]                 // Saknar liten bokstav
    [DataRow("validuser", "NoDigits!@", false)]                     // Saknar siffra
    [DataRow("validuser", "NoSpecial123", false)]                   // Saknar specialtecken
    [DataRow("validuser", "Valid123@", true)]                       // Giltigt lösenord och användarnamn
    [DataRow("user_123", "PassWord1!", true)]                       // Giltigt användarnamn med underscore
    public void ValidateUserDto_ShouldReturnExpected(string username, string password, bool expectedIsValid)
    {
        // Given a user DTO with a username and password
        var userDto = new UserDto
        {
            Username = username,
            Password = password
        };

        // When validating the user DTO
        var method = typeof(AuthService).GetMethod("ValidateUserDto", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var isValid = (bool)method.Invoke(_authService, new object[] { userDto });

        // Then the validation result should match the expected result
        Assert.AreEqual(expectedIsValid, isValid);
    }

}
