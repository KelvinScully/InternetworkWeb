using Common.Objects;
using DataAccessLayer.Objects.Inventory;

namespace DataAccessLayer.Interfaces
{
    public interface IInventoryDalService
    {
        Task<ApiResult<ItemStatusApo>> ItemStatusGet(int itemStatusId);
        Task<ApiResult<List<ItemStatusApo>>> ItemStatusGet();
    }
}
