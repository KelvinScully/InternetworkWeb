using Common.Objects;
using DataAccessLayer.Objects.Inventory;

namespace Repository.Interfaces
{
    public interface IInventoryRepoService
    {
        // Item Category
        Task<ApiResult<List<ItemApo>>> InventoryItemGet(bool returnInactive);
        Task<ApiResult<ItemApo>> InventoryItemGet(int itemId);
        Task<ApiResult<bool>> InventoryItemInsert(ItemApo itemApo);
        Task<ApiResult<bool>> InventoryItemUpdate(ItemApo itemApo);
        Task<ApiResult<bool>> InventoryItemDelete(int itemId);

        // Item Category
        Task<ApiResult<List<ItemCategoryApo>>> InventoryItemCategoryGet(bool returnInactive);
        Task<ApiResult<ItemCategoryApo>> InventoryItemCategoryGet(int itemCategoryId);
        Task<ApiResult<bool>> InventoryItemCategoryInsert(ItemCategoryApo itemCategoryApo);
        Task<ApiResult<bool>> InventoryItemCategoryUpdate(ItemCategoryApo itemCategoryApo);
        Task<ApiResult<bool>> InventoryItemCategoryDelete(int itemCategoryId);

        // Item Location
        Task<ApiResult<List<ItemLocationApo>>> InventoryItemLocationGet(bool returnInactive);
        Task<ApiResult<ItemLocationApo>> InventoryItemLocationGet(int itemLocationId);
        Task<ApiResult<bool>> InventoryItemLocationInsert(ItemLocationApo itemLocationApo);
        Task<ApiResult<bool>> InventoryItemLocationUpdate(ItemLocationApo itemLocationApo);
        Task<ApiResult<bool>> InventoryItemLocationDelete(int itemLocationId);

        // Item Status
        Task<ApiResult<List<ItemStatusApo>>> InventoryItemStatusGet(bool returnInactive);
        Task<ApiResult<ItemStatusApo>> InventoryItemStatusGet(int itemStatusId);
        Task<ApiResult<bool>> InventoryItemStatusInsert(ItemStatusApo itemStatusApo);
        Task<ApiResult<bool>> InventoryItemStatusUpdate(ItemStatusApo itemStatusApo);
        Task<ApiResult<bool>> InventoryItemStatusDelete(int itemStatusId);
    }
}
