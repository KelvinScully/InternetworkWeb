namespace DataAccessLayer.Objects.Inventory
{
    // APO for Item Status
    // APO: Application Programming Object. Custom Name, don't look to far into it.
    public class ItemCategoryApo
    {
        public int ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
