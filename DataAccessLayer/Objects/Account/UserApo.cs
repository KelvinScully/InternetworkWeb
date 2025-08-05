namespace DataAccessLayer.Objects.Account
{
    // APO for Item Status
    // APO: Application Programming Object. Custom Name, don't look to far into it.
    public class UserApo
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
        public List<UserRoleApo> UserRoles { get; set; } = [];

        public bool IsDefault()
        {
            if (UserId == 0 && string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(UserEmail)) { return true; }
            return false;
        }
    }
}
