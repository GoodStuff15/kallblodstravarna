﻿using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public interface IAuthService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<RegisterResult> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(TokenRefreshDto request);
        Task<User?> GetUserByUsernameAsync(string username);
        Task ClearRefreshTokenAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
