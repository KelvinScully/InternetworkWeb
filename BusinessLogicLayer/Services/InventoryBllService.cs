using BusinessLogicLayer.Interfaces;
using Common.Objects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Inventory;


namespace BusinessLogicLayer.Services
{
    internal class InventoryBllService(IInventoryDalService dal) : IInventoryBllService
    {
        private IInventoryDalService _Dal = dal;

        public async Task<ApiResult<ItemStatusApo>> GetItemStatus(int itemStatusId)
        {
            if (itemStatusId == 0)
            {
                return new ApiResult<ItemStatusApo>
                {
                    IsSuccessful = false,
                    Value = new ItemStatusApo(),
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.ItemStatusGet(itemStatusId);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<ItemStatusApo>
                    {
                        IsSuccessful = false,
                        Value = new ItemStatusApo(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<ItemStatusApo>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemStatusApo>
                {
                    IsSuccessful = false,
                    Value = new ItemStatusApo(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<List<ItemStatusApo>>> GetItemStatus()
        {

            try
            {
                var dalResult = await _Dal.ItemStatusGet();

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<List<ItemStatusApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<List<ItemStatusApo>>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Objects Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemStatusApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
    }
}
