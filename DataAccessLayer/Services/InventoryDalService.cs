using Common;
using Common.Objects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Inventory;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Services
{
    internal class InventoryDalService(ConnectionOptions connectionOptions) : DataAccessService(connectionOptions), IInventoryDalService
    {
        public async Task<ApiResult<ItemStatusApo>> ItemStatusGet(int itemStatusId)
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@ItemStatusId", itemStatusId)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["ItemStatusId"] = "ItemStatusId",
                ["ItemStatusName"] = "ItemStatusName",
                ["IsActive"] = "IsActive"
            };
            try
            {
                var itemStatus = await GetSqlSingleAsync<ItemStatusApo>("Inventory.SpItemStatusGet", parameters, propertyMap);

                return new ApiResult<ItemStatusApo>()
                {
                    IsSuccessful = true,
                    Value = itemStatus,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemStatusApo>
                {
                    IsSuccessful = false,
                    Value = new ItemStatusApo(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<List<ItemStatusApo>>> ItemStatusGet()
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@ItemStatusId", 0)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["ItemStatusId"] = "ItemStatusId",
                ["ItemStatusName"] = "ItemStatusName",
                ["IsActive"] = "IsActive"
            };
            try
            {
                var itemStatuses = await GetSqlListAsync<ItemStatusApo>("Inventory.SpItemStatusGet", parameters, propertyMap);

                return new ApiResult<List<ItemStatusApo>>()
                {
                    IsSuccessful = true,
                    Value = itemStatuses,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemStatusApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
    }
}
