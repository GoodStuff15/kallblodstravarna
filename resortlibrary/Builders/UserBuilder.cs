using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class UserBuilder
    {
        private User _user;

        public UserBuilder()
        {
            _user = new User();
        }

        public UserBuilder WithUsername(string username)//Set username. Cant be null or whitespace
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username must be entered.");
            }

            _user.Username = username;
            return this;
        }

        public UserBuilder WithPasswordHash(string passwordHash)//Set password. Cant be null or whitespace
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password must be entered.");                
            }

            _user.PasswordHash = passwordHash;
            return this;
        }

        public UserBuilder WithRole(string role)//Set user role. Cant be null or whitespace
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Role must be entered.");
            }

            _user.Role = role;
            return this;
        }

        public UserBuilder WithCustomer(Customer customer)//Optionally associates the cuser with a customper.
        {
            _user.Customer = customer;
            return this;
        }

        public UserBuilder WithRefreshToken(string refreshToken)//Optionally sets the refresh token
        {
            _user.RefreshToken = refreshToken;
            return this;
        }

        public UserBuilder WithRefreshTokenExpiry(DateTime expiryTime)//Optionally sets the refresh token expiry time
        {
            _user.RefreshTokenExpiryTime = expiryTime;
            return this;
        }

        public User Build()//Build user-object
        {
            if (string.IsNullOrWhiteSpace(_user.Username))
            {
                throw new InvalidOperationException("Username is not entered.");
            }
                
            if (string.IsNullOrWhiteSpace(_user.PasswordHash))
            {
                throw new InvalidOperationException("Password is not entered.");
            }
                
            if (string.IsNullOrWhiteSpace(_user.Role))
            {
                throw new InvalidOperationException("Role is not entered.");
            }

            return _user;
        }
    }
}
