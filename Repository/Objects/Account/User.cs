using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Objects.Account
{
    // Requested Objects
    public class Register
    {
        public string Username { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
    }
    public class Authenticate
    {
        public string Username { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }

    // Returned Objects
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = [];
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }

        public bool IsDefault()
        {
            if (UserId == 0 && string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(UserEmail)) { return true; }
            return false;
        }
    }
}