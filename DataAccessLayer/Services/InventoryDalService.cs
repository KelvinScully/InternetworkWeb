using Common;
using Common.Objects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Objects.Inventory;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Services
{
    internal class InventoryDalService(ConnectionOptions connectionOptions) : DataAccessService(connectionOptions), IInventoryDalService
    {
        #region Item

        public async Task<ApiResult<List<ItemApo>>> ItemGet()
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@ItemId", 0)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["ItemId"] = "ItemId",
                ["ItemName"] = "ItemName",
                ["ItemQuantity"] = "ItemQuantity",

                ["CategoryId"] = "CategoryId",
                ["ItemCategoryName"] = "ItemCategoryName",
                ["LocationId"] = "LocationId",
                ["ItemLocationName"] = "ItemLocationName",
                ["StatusId"] = "StatusId",
                ["ItemStatusName"] = "ItemStatusName",

                ["IsActive"] = "IsActive"
            };
            try
            {
                var items = await GetSqlListAsync<ItemApo>("Inventory.SpItemGet", parameters, propertyMap);

                return new ApiResult<List<ItemApo>>()
                {
                    IsSuccessful = true,
                    Value = items,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<ItemApo>> ItemGet(int itemId)
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@ItemId", itemId)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["ItemId"] = "ItemId",
                ["ItemName"] = "ItemName",
                ["ItemQuantity"] = "ItemQuantity",

                ["CategoryId"] = "CategoryId",
                ["ItemCategoryName"] = "ItemCategoryName",
                ["LocationId"] = "LocationId",
                ["ItemLocationName"] = "ItemLocationName",
                ["StatusId"] = "StatusId",
                ["ItemStatusName"] = "ItemStatusName",

                ["IsActive"] = "IsActive"
            };
            try
            {
                var item = await GetSqlSingleAsync<ItemApo>("Inventory.SpItemGet", parameters, propertyMap);

                return new ApiResult<ItemApo>()
                {
                    IsSuccessful = true,
                    Value = item,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemInsert(ItemApo itemApo)
        {
            SqlParameter[] parameters =
            [
                new("@ItemName", itemApo.ItemName),
                new("@ItemQuantity", itemApo.ItemQuantity),
                new("@CategoryId", itemApo.CategoryId),
                new("@LocationId", itemApo.LocationId),
                new("@StatusId", itemApo.StatusId)
            ];

            try
            {
                var isDBSuccessful = await InsertSqlAsync("Inventory.SpItemInsert", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemUpdate(ItemApo itemApo)
        {
            SqlParameter[] parameters =
            [
                new("@ItemId", itemApo.ItemId),
                new("@ItemName", itemApo.ItemName),
                new("@ItemQuantity", itemApo.ItemQuantity),
                new("@CategoryId", itemApo.CategoryId),
                new("@LocationId", itemApo.LocationId),
                new("@StatusId", itemApo.StatusId)
            ];

            try
            {
                var isDBSuccessful = await UpdateSqlAsync("Inventory.SpItemUpdate", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Update"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemDelete(int itemId)
        {
            SqlParameter[] parameters =
            [
                new("@ItemId", itemId)
            ];

            try
            {
                var isDBSuccessful = await DeleteSqlAsync("Inventory.SpItemDeleteSoft", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }

        #endregion

        #region Item Category

        public async Task<ApiResult<List<ItemCategoryApo>>> ItemCategoryGet()
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@ItemCategoryId", 0)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["ItemCategoryId"] = "ItemCategoryId",
                ["ItemCategoryName"] = "ItemCategoryName",
                ["IsActive"] = "IsActive"
            };
            try
            {
                var itemCategories = await GetSqlListAsync<ItemCategoryApo>("Inventory.SpItemCategoryGet", parameters, propertyMap);

                return new ApiResult<List<ItemCategoryApo>>()
                {
                    IsSuccessful = true,
                    Value = itemCategories,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemCategoryApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<ItemCategoryApo>> ItemCategoryGet(int itemCategoryId)
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@ItemCategoryId", itemCategoryId)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["ItemCategoryId"] = "ItemCategoryId",
                ["ItemCategoryName"] = "ItemCategoryName",
                ["IsActive"] = "IsActive"
            };
            try
            {
                var itemCategory = await GetSqlSingleAsync<ItemCategoryApo>("Inventory.SpItemCategoryGet", parameters, propertyMap);

                return new ApiResult<ItemCategoryApo>()
                {
                    IsSuccessful = true,
                    Value = itemCategory,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemCategoryApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemCategoryInsert(ItemCategoryApo itemCategoryApo)
        {
            SqlParameter[] parameters =
            [
                new("@ItemCategoryName", itemCategoryApo.ItemCategoryName)
            ];

            try
            {
                var isDBSuccessful = await InsertSqlAsync("Inventory.SpItemCategoryInsert", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemCategoryUpdate(ItemCategoryApo itemCategoryApo)
        {
            SqlParameter[] parameters =
            [
                new("@ItemCategoryId", itemCategoryApo.ItemCategoryId),
                new("@ItemCategoryName", itemCategoryApo.ItemCategoryName)
            ];

            try
            {
                var isDBSuccessful = await UpdateSqlAsync("Inventory.SpItemCategoryUpdate", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Update"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemCategoryDelete(int itemCategoryId)
        {
            SqlParameter[] parameters =
            [
                new("@ItemCategoryId", itemCategoryId)
            ];

            try
            {
                var isDBSuccessful = await DeleteSqlAsync("Inventory.SpItemCategoryDeleteSoft", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }

        #endregion

        #region Item Location

        public async Task<ApiResult<List<ItemLocationApo>>> ItemLocationGet()
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@ItemLocationId", 0)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["ItemLocationId"] = "ItemLocationId",
                ["ItemLocationName"] = "ItemLocationName",
                ["IsActive"] = "IsActive"
            };
            try
            {
                var itemLocations = await GetSqlListAsync<ItemLocationApo>("Inventory.SpItemLocationGet", parameters, propertyMap);

                return new ApiResult<List<ItemLocationApo>>()
                {
                    IsSuccessful = true,
                    Value = itemLocations,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemLocationApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<ItemLocationApo>> ItemLocationGet(int itemLocationId)
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@ItemLocationId", itemLocationId)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["ItemLocationId"] = "ItemLocationId",
                ["ItemLocationName"] = "ItemLocationName",
                ["IsActive"] = "IsActive"
            };
            try
            {
                var itemLocation = await GetSqlSingleAsync<ItemLocationApo>("Inventory.SpItemLocationGet", parameters, propertyMap);

                return new ApiResult<ItemLocationApo>()
                {
                    IsSuccessful = true,
                    Value = itemLocation,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemLocationApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemLocationInsert(ItemLocationApo itemLocationApo)
        {
            SqlParameter[] parameters =
            [
                new("@ItemLocationName", itemLocationApo.ItemLocationName)
            ];

            try
            {
                var isDBSuccessful = await InsertSqlAsync("Inventory.SpItemLocationInsert", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemLocationUpdate(ItemLocationApo itemLocationApo)
        {
            SqlParameter[] parameters =
            [
                new("@ItemLocationId", itemLocationApo.ItemLocationId),
                new("@ItemLocationName", itemLocationApo.ItemLocationName)
            ];

            try
            {
                var isDBSuccessful = await UpdateSqlAsync("Inventory.SpItemLocationUpdate", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Update"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemLocationDelete(int itemLocationId)
        {
            SqlParameter[] parameters =
            [
                new("@ItemLocationId", itemLocationId)
            ];

            try
            {
                var isDBSuccessful = await DeleteSqlAsync("Inventory.SpItemLocationDeleteSoft", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }

        #endregion

        #region Item Status

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
        public async Task<ApiResult<bool>> ItemStatusInsert(ItemStatusApo itemStatusApo)
        {
            SqlParameter[] parameters =
            [
                new("@ItemStatusName", itemStatusApo.ItemStatusName)
            ];

            try
            {
                var isDBSuccessful = await InsertSqlAsync("Inventory.SpItemStatusInsert", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemStatusUpdate(ItemStatusApo itemStatusApo)
        {
            SqlParameter[] parameters =
            [
                new("@ItemStatusId", itemStatusApo.ItemStatusId),
                new("@ItemStatusName", itemStatusApo.ItemStatusName)
            ];

            try
            {
                var isDBSuccessful = await UpdateSqlAsync("Inventory.SpItemStatusUpdate", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Update"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemStatusDelete(int itemStatusId)
        {
            SqlParameter[] parameters =
            [
                new("@ItemStatusId", itemStatusId)
            ];

            try
            {
                var isDBSuccessful = await DeleteSqlAsync("Inventory.SpItemStatusDeleteSoft", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }

        #endregion
    }
}
