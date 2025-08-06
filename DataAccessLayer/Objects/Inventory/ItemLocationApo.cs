namespace DataAccessLayer.Objects.Inventory
{
    // APO for Item Status
    // APO: Application Programming Object. Custom Name, don't look to far into it.
    public class ItemLocationApo
    {
        public int ItemLocationId { get; set; }
        public string ItemLocationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
