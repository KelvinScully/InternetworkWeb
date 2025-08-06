namespace MvcApp.areas.Inventory.Models
{
    public class ItemLocationModel
    {
        public int ItemLocationId { get; set; }
        public string ItemLocationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
