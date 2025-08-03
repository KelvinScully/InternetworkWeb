using BusinessLogicLayer.Interfaces;
using Common;
using Common.Objects;
using DataAccessLayer.Objects.Inventory;
using Repository.Interfaces;

namespace Repository.Services
{
    internal class InventoryRepoServices(ConnectionOptions options, IInventoryBllService bll) : RepositoryBase(options), IInventoryRepoService
    {

        private readonly IInventoryBllService _Bll = bll;
        public async Task<ApiResult<ItemStatusApo>> InventoryGetItemStatus(int itemStatusId)
        {
            try
            {
                var result = await _Bll.GetItemStatus(itemStatusId);
                return result ?? new ApiResult<ItemStatusApo> { IsSuccessful = false, Value = new(), Message = "Empty result" };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemStatusApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<List<ItemStatusApo>>> InventoryGetItemStatus()
        {
            try
            {
                var result = await _Bll.GetItemStatus();
                return result ?? new ApiResult<List<ItemStatusApo>> { IsSuccessful = false, Value = [], Message = "Empty result" };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemStatusApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
    }
}
