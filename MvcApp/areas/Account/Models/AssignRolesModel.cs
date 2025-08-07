namespace MvcApp.Areas.Account.Models
{
    /// ViewModel for Assigning Roles to a User.
    public class AssignRolesModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        // New: only one role may be selected
        public int? SelectedRoleId { get; set; }

        // just the list of choices
        public List<RoleItem> Roles { get; set; } = new();

        public bool ShowInactive { get; set; }

        public class RoleItem
        {
            public int UserRoleId { get; set; }
            public string UserRoleName { get; set; } = string.Empty;
        }
    }
}
