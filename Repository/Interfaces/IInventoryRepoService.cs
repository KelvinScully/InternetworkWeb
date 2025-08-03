using Common.Objects;
using DataAccessLayer.Objects.Inventory;

namespace Repository.Interfaces
{
    public interface IInventoryRepoService
    {
        Task<ApiResult<ItemStatusApo>> InventoryGetItemStatus(int itemStatusId);
        Task<ApiResult<List<ItemStatusApo>>> InventoryGetItemStatus();
    }
}
