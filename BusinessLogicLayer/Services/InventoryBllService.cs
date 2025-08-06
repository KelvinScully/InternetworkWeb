using BusinessLogicLayer.Interfaces;
using Common.Objects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Inventory;


namespace BusinessLogicLayer.Services
{
    internal class InventoryBllService(IInventoryDalService dal) : IInventoryBllService
    {
        private readonly IInventoryDalService _Dal = dal;

        #region Item

        public async Task<ApiResult<List<ItemApo>>> ItemGet()
        {
            try
            {
                var dalResult = await _Dal.ItemGet();

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<List<ItemApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<List<ItemApo>>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Objects Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<ItemApo>> ItemGet(int itemd)
        {
            if (itemd == 0)
            {
                return new ApiResult<ItemApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.ItemGet(itemd);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<ItemApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<ItemApo>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemInsert(ItemApo itemApo)
        {
            try
            {
                var dalResult = await _Dal.ItemInsert(itemApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemUpdate(ItemApo itemApo)
        {
            try
            {
                var dalResult = await _Dal.ItemUpdate(itemApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemDelete(int itemId)
        {
            if (itemId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.ItemDelete(itemId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }

        #endregion

        #region Item Category

        public async Task<ApiResult<List<ItemCategoryApo>>> ItemCategoryGet()
        {
            try
            {
                var dalResult = await _Dal.ItemCategoryGet();

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<List<ItemCategoryApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<List<ItemCategoryApo>>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Objects Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemCategoryApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<ItemCategoryApo>> ItemCategoryGet(int itemCategoryId)
        {
            if (itemCategoryId == 0)
            {
                return new ApiResult<ItemCategoryApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.ItemCategoryGet(itemCategoryId);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<ItemCategoryApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<ItemCategoryApo>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemCategoryApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemCategoryInsert(ItemCategoryApo itemCategoryApo)
        {
            try
            {
                var dalResult = await _Dal.ItemCategoryInsert(itemCategoryApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemCategoryUpdate(ItemCategoryApo itemCategoryApo)
        {
            try
            {
                var dalResult = await _Dal.ItemCategoryUpdate(itemCategoryApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemCategoryDelete(int itemCategoryId)
        {
            if (itemCategoryId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.ItemCategoryDelete(itemCategoryId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }

        #endregion

        #region Item Location

        public async Task<ApiResult<List<ItemLocationApo>>> ItemLocationGet()
        {
            try
            {
                var dalResult = await _Dal.ItemLocationGet();

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<List<ItemLocationApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<List<ItemLocationApo>>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Objects Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<ItemLocationApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<ItemLocationApo>> ItemLocationGet(int itemLocationId)
        {
            if (itemLocationId == 0)
            {
                return new ApiResult<ItemLocationApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.ItemLocationGet(itemLocationId);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<ItemLocationApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<ItemLocationApo>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<ItemLocationApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemLocationInsert(ItemLocationApo itemLocationApo)
        {
            try
            {
                var dalResult = await _Dal.ItemLocationInsert(itemLocationApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemLocationUpdate(ItemLocationApo itemLocationApo)
        {
            try
            {
                var dalResult = await _Dal.ItemLocationUpdate(itemLocationApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemLocationDelete(int itemLocationId)
        {
            if (itemLocationId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.ItemLocationDelete(itemLocationId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }

        #endregion

        #region Item Status

        public async Task<ApiResult<List<ItemStatusApo>>> ItemStatusGet()
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
        public async Task<ApiResult<ItemStatusApo>> ItemStatusGet(int itemStatusId)
        {
            if (itemStatusId == 0)
            {
                return new ApiResult<ItemStatusApo>
                {
                    IsSuccessful = false,
                    Value = new(),
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
                        Value = new(),
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
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemStatusInsert(ItemStatusApo itemStatusApo)
        {
            try
            {
                var dalResult = await _Dal.ItemStatusInsert(itemStatusApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemStatusUpdate(ItemStatusApo itemStatusApo)
        {
            try
            {
                var dalResult = await _Dal.ItemStatusUpdate(itemStatusApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> ItemStatusDelete(int itemStatusId)
        {
            if (itemStatusId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.ItemStatusDelete(itemStatusId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }

        #endregion
    }
}
