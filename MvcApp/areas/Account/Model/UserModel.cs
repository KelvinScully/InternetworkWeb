using System.Text.Json.Serialization;

namespace MvcApp.areas.Account.Model
{
    public class LoginRegisterModel
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; } = 0;
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("rememberMe")]
        public bool RememberMe { get; set; } = false;

        public bool IsNullOrEmptyForLogin()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) { return true; } 
            return false;
        }
        public bool IsNullOrEmptyForRegistration()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email)) { return true; }
            return false;
        }
        public bool IsDefault()
        {
            if (UserId == 0 && string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(Email) && RememberMe == false) { return true; } 
            return false;
        }
    }
}
