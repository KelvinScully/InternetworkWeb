namespace DataAccessLayer.Objects.Inventory
{
    // APO for Item Status
    // APO: Application Programming Object. Custom Name, don't look to far into it.
    public class ItemApo
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal ItemQuantity { get; set; }

        public int CategoryId { get; set; }
        public string ItemCategoryName { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string ItemLocationName { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string ItemStatusName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
