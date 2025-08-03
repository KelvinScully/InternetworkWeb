namespace MvcApp.areas.Inventory.Models
{
    public class ItemStatusModel
    {
        public int ItemStatusId { get; set; }
        public string ItemStatusName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}