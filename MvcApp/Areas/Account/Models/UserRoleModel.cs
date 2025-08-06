namespace MvcApp.Areas.Account.Models
{
    public class UserRoleModel
    {
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool ShowInactive { get; set; }
    }
}
