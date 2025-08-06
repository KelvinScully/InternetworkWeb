using Common.Objects;
using DataAccessLayer.Objects.Inventory;

namespace BusinessLogicLayer.Interfaces
{
    public interface IInventoryBllService
    {
        // Item
        Task<ApiResult<List<ItemApo>>> ItemGet();
        Task<ApiResult<ItemApo>> ItemGet(int itemd);
        Task<ApiResult<bool>> ItemInsert(ItemApo itemApo);
        Task<ApiResult<bool>> ItemUpdate(ItemApo itemApo);
        Task<ApiResult<bool>> ItemDelete(int itemId);

        // Item Category
        Task<ApiResult<List<ItemCategoryApo>>> ItemCategoryGet();
        Task<ApiResult<ItemCategoryApo>> ItemCategoryGet(int itemCategoryId);
        Task<ApiResult<bool>> ItemCategoryInsert(ItemCategoryApo itemCategoryApo);
        Task<ApiResult<bool>> ItemCategoryUpdate(ItemCategoryApo itemCategoryApo);
        Task<ApiResult<bool>> ItemCategoryDelete(int itemCategoryId);

        // Item Location
        Task<ApiResult<List<ItemLocationApo>>> ItemLocationGet();
        Task<ApiResult<ItemLocationApo>> ItemLocationGet(int itemLocationId);
        Task<ApiResult<bool>> ItemLocationInsert(ItemLocationApo itemLocationApo);
        Task<ApiResult<bool>> ItemLocationUpdate(ItemLocationApo itemLocationApo);
        Task<ApiResult<bool>> ItemLocationDelete(int itemLocationId);

        // Item Status
        Task<ApiResult<List<ItemStatusApo>>> ItemStatusGet();
        Task<ApiResult<ItemStatusApo>> ItemStatusGet(int itemStatusId);
        Task<ApiResult<bool>> ItemStatusInsert(ItemStatusApo itemStatusApo);
        Task<ApiResult<bool>> ItemStatusUpdate(ItemStatusApo itemStatusApo);
        Task<ApiResult<bool>> ItemStatusDelete(int itemStatusId);
    }
}
