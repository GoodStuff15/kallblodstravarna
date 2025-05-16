using resortdtos;
using restortlibrary.Models;

namespace resortapi.Services
{
    public interface IAuthService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(TokenRefreshDto request);
        Task<User?> GetUserByUsernameAsync(string username);
        Task ClearRefreshTokenAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
    }
}
