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

        #region Item

        public async Task<ApiResult<List<ItemApo>>> InventoryItemGet()
        {
            try
            {
                var result = await _Bll.ItemGet();
                if (result == null || !result.IsSuccessful || result.Value == null || result.Value.Count == 0)
                {
                    return new ApiResult<List<ItemApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }    
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<ItemApo>> InventoryItemGet(int itemId)
        {
            try
            {
                var result = await _Bll.ItemGet(itemId);
                if (result == null || !result.IsSuccessful || result.Value == null)
                {
                    return new ApiResult<ItemApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemInsert(ItemApo itemApo)
        {
            try
            {
                var result = await _Bll.ItemInsert(itemApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Inserted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Insert Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemUpdate(ItemApo itemApo)
        {
            try
            {
                var result = await _Bll.ItemUpdate(itemApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Updated"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Update Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemDelete(int itemId)
        {
            try
            {
                var result = await _Bll.ItemDelete(itemId);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Deleted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Delete Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        #endregion

        #region Item Category

        public async Task<ApiResult<List<ItemCategoryApo>>> InventoryItemCategoryGet()
        {
            try
            {
                var result = await _Bll.ItemCategoryGet();
                if (result == null || !result.IsSuccessful || result.Value == null || result.Value.Count == 0)
                {
                    return new ApiResult<List<ItemCategoryApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemCategoryApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<ItemCategoryApo>> InventoryItemCategoryGet(int itemCategoryId)
        {
            try
            {
                var result = await _Bll.ItemCategoryGet(itemCategoryId);
                if (result == null || !result.IsSuccessful || result.Value == null)
                {
                    return new ApiResult<ItemCategoryApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemCategoryApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemCategoryInsert(ItemCategoryApo itemCategoryApo)
        {
            try
            {
                var result = await _Bll.ItemCategoryInsert(itemCategoryApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Category Inserted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Category Insert Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemCategoryUpdate(ItemCategoryApo itemCategoryApo)
        {
            try
            {
                var result = await _Bll.ItemCategoryUpdate(itemCategoryApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Category Updated"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Category Update Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemCategoryDelete(int itemCategoryId)
        {
            try
            {
                var result = await _Bll.ItemCategoryDelete(itemCategoryId);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Category Deleted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Category Delete Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        #endregion

        #region Item Location

        public async Task<ApiResult<List<ItemLocationApo>>> InventoryItemLocationGet()
        {
            try
            {
                var result = await _Bll.ItemLocationGet();
                if (result == null || !result.IsSuccessful || result.Value == null || result.Value.Count == 0)
                {
                    return new ApiResult<List<ItemLocationApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemLocationApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<ItemLocationApo>> InventoryItemLocationGet(int itemLocationId)
        {
            try
            {
                var result = await _Bll.ItemLocationGet(itemLocationId);
                if (result == null || !result.IsSuccessful || result.Value == null)
                {
                    return new ApiResult<ItemLocationApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemLocationApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemLocationInsert(ItemLocationApo itemLocationApo)
        {
            try
            {
                var result = await _Bll.ItemLocationInsert(itemLocationApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Location Inserted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Location Insert Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemLocationUpdate(ItemLocationApo itemLocationApo)
        {
            try
            {
                var result = await _Bll.ItemLocationUpdate(itemLocationApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Location Updated"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Location Update Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemLocationDelete(int itemLocationId)
        {
            try
            {
                var result = await _Bll.ItemLocationDelete (itemLocationId);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Location Deleted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Location Delete Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        #endregion

        #region Item Status

        public async Task<ApiResult<List<ItemStatusApo>>> InventoryItemStatusGet()
        {
            try
            {
                var result = await _Bll.ItemStatusGet();
                if (result == null || !result.IsSuccessful || result.Value == null || result.Value.Count == 0)
                {
                    return new ApiResult<List<ItemStatusApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }
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
        public async Task<ApiResult<ItemStatusApo>> InventoryItemStatusGet(int itemStatusId)
        {
            try
            {
                var result = await _Bll.ItemStatusGet(itemStatusId);
                if (result == null || !result.IsSuccessful || result.Value == null)
                {
                    return new ApiResult<ItemStatusApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }
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
        public async Task<ApiResult<bool>> InventoryItemStatusInsert(ItemStatusApo itemStatusApo)
        {
            try
            {
                var result = await _Bll.ItemStatusInsert(itemStatusApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Status Inserted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Status Insert Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemStatusUpdate(ItemStatusApo itemStatusApo)
        {
            try
            {
                var result = await _Bll.ItemStatusUpdate(itemStatusApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Status Updated"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Status Update Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> InventoryItemStatusDelete(int itemStatusId)
        {
            try
            {
                var result = await _Bll.ItemStatusDelete(itemStatusId);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"Item Status Deleted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"Item Status Delete Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        #endregion
    }
}
