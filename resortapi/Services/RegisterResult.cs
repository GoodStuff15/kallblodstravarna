using restortlibrary.Models;

namespace resortapi.Services
{
    public class RegisterResult
    {
        public User? User { get; set; }
        public string? Error { get; set; }

        public static RegisterResult Success(User user) => new() { User = user };
        public static RegisterResult Fail(string error) => new() { Error = error };

    }
}
