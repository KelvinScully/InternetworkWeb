using Common.Objects;
using DataAccessLayer.Objects.Inventory;

namespace BusinessLogicLayer.Interfaces
{
    public interface IInventoryBllService
    {
        Task<ApiResult<ItemStatusApo>> GetItemStatus(int itemStatusId);
        Task<ApiResult<List<ItemStatusApo>>> GetItemStatus();
    }
}
