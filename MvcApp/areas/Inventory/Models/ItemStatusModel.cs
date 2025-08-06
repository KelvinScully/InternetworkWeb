namespace MvcApp.Areas.Inventory.Models
{
    public class ItemStatusModel
    {
        public int ItemStatusId { get; set; }
        public string ItemStatusName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}