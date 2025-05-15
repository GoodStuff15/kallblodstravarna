using resortdtos;
using restortlibrary.Models;

namespace restortlibrary.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(TokenRefreshDto request);

    }
}
