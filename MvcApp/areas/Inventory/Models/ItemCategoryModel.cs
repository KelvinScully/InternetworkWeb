namespace MvcApp.Areas.Inventory.Models
{
    public class ItemCategoryModel
    {
        public int ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
