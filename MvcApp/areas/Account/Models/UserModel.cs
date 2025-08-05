using DataAccessLayer.Objects.Account;
using System.Text.Json.Serialization;

namespace MvcApp.areas.Account.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        // Password will not be return, it's not even stored
        public string Password { get; set; } = string.Empty;
        public byte[] UserHash { get; set; } = [];
        public byte[] UserSalt { get; set; } = [];
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
        public bool RememberMe { get; set; } = false;
        public List<UserRoleApo> UserRoles { get; set; } = [];
        public bool IsDefault()
        {
            if (UserId == 0 && string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(UserEmail) && RememberMe == false) { return true; }
            return false;
        }
    }

    public class LoginRegisterModel
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; } = 0;
        [JsonPropertyName("username")]
        public string UserName { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string UserEmail { get; set; } = string.Empty;
        [JsonPropertyName("rememberMe")]
        public bool RememberMe { get; set; } = false;

        public bool IsNullOrEmptyForLogin()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)) { return true; }
            return false;
        }
        public bool IsNullOrEmptyForRegistration()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(UserEmail)) { return true; }
            return false;
        }
        public bool IsDefault()
        {
            if (UserId == 0 && string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(UserEmail) && RememberMe == false) { return true; }
            return false;
        }
    }
}