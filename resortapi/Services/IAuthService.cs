using resortdtos;
using restortlibrary.Models;

namespace restortlibrary.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);

    }
}
