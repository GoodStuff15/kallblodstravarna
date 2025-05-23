using System.ComponentModel.DataAnnotations;

namespace restortlibrary.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public int CustomerId { get; set; } = 0;
        public Customer? Customer { get; set; } = null;

    }
}
