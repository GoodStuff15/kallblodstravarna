using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using resortdtos;
using resortlibrary.Models;
using resortapi.Services;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var result = await authService.RegisterAsync(request);

            if (result.User == null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.User);
        }


        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync(request);

            if (result is null)
            {
                return BadRequest("Invalid username and/or password");
            }

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(TokenRefreshDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                return Unauthorized("Invalid refresh token");
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            var username = User.Identity?.Name;

            return Ok($"{username} is authenticated!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            var username = User.Identity?.Name;

            return Ok($"{username} is an admin!");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity?.Name;
            var user = await authService.GetUserByUsernameAsync(username!);

            if (user == null)
                return NotFound();

            await authService.ClearRefreshTokenAsync(user);

            return Ok($"{username} logged out successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = await authService.GetUserByIdAsync(id);

            if (userToDelete is null)
                return NotFound("User not found.");

            var success = await authService.DeleteUserAsync(id);

            if (!success)
                return StatusCode(500, "Failed to delete user.");

            return Ok($"User '{userToDelete.Username}' was deleted by admin '{User.Identity?.Name}'.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await authService.GetAllUsersAsync();
            return Ok(users);
        }

    }
}
