namespace DataAccessLayer.Objects.Inventory
{
    // APO for Item Status
    // APO: Application Programming Object. Custom Name, don't look to far into it.
    public class ItemStatusApo
    {
        public int ItemStatusId { get; set; }
        public string ItemStatusName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
