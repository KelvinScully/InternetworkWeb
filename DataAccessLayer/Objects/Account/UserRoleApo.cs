namespace DataAccessLayer.Objects.Account
{
    // APO for Item Status
    // APO: Application Programming Object. Custom Name, don't look to far into it.
    public class UserRoleApo
    {
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
