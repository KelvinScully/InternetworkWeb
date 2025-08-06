namespace MvcApp.Areas.Inventory.Models
{
    public class ItemModel
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
