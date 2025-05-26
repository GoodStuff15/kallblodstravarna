//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using resortdtos;
//using resortapi.Data;
//using resortapi.Services;

//namespace AuthServiceTests
//{
//    [TestClass]
//    public class AuthServiceUnitTests
//    {
//        private ResortContext _context = null!;
//        private AuthService _authService = null!;
//        private IConfiguration _configuration = null!;

//        [TestInitialize]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<ResortContext>()
//                .UseInMemoryDatabase(databaseName: "TestDb")
//                .Options;

//            _context = new ResortContext(options);

//            var inMemorySettings = new Dictionary<string, string> {
//                {"AppSettings:Token", "supersecrettokenkey123"},
//                {"AppSettings:Issuer", "TestIssuer"},
//                {"AppSettings:Audience", "TestAudience"}
//            };

//            _configuration = new ConfigurationBuilder()
//                .AddInMemoryCollection(inMemorySettings!)
//                .Build();

//            _authService = new AuthService(_context, _configuration);
//        }

//        [TestMethod]
//        public async Task RegisterAsync_ShouldRegisterUser_WhenUsernameIsUnique()
//        {
//            var userDto = new UserDto
//            {
//                Username = "newuser",
//                Password = "password",
//                Role = "User"
//            };

//            var user = await _authService.RegisterAsync(userDto);

//            Assert.IsNotNull(user);
//            Assert.AreEqual("newuser", user?.Username);
//        }

//        [TestMethod]
//        public async Task RegisterAsync_ShouldReturnNull_WhenUsernameExists()
//        {
//            var userDto = new UserDto { Username = "duplicate", Password = "pass", Role = "User" };
//            await _authService.RegisterAsync(userDto);

//            var result = await _authService.RegisterAsync(userDto);

//            Assert.IsNull(result);
//        }

//        [TestMethod]
//        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreCorrect()
//        {
//            var userDto = new UserDto { Username = "tester", Password = "test123", Role = "User" };
//            await _authService.RegisterAsync(userDto);

//            var result = await _authService.LoginAsync(userDto);

//            Assert.IsNotNull(result);
//            Assert.IsFalse(string.IsNullOrEmpty(result?.AccessToken));
//        }

//        [TestMethod]
//        public async Task LoginAsync_ShouldReturnNull_WhenPasswordIsIncorrect()
//        {
//            var userDto = new UserDto { Username = "tester2", Password = "rightpass", Role = "User" };
//            await _authService.RegisterAsync(userDto);

//            var wrongLogin = new UserDto { Username = "tester2", Password = "wrongpass" };

//            var result = await _authService.LoginAsync(wrongLogin);

//            Assert.IsNull(result);
//        }

//        [TestMethod]
//        public async Task RefreshTokensAsync_ShouldReturnNewTokens_WhenRefreshTokenIsValid()
//        {
//            var userDto = new UserDto { Username = "refreshUser", Password = "pass123", Role = "User" };
//            var user = await _authService.RegisterAsync(userDto);
//            var loginResult = await _authService.LoginAsync(userDto);

//            var refreshDto = new TokenRefreshDto
//            {
//                UserId = user!.Id,
//                RefreshToken = loginResult!.RefreshToken
//            };

//            var result = await _authService.RefreshTokensAsync(refreshDto);

//            Assert.IsNotNull(result);
//            Assert.AreNotEqual(loginResult.AccessToken, result.AccessToken);
//        }
//    }
//}
