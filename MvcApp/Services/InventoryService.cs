using AutoMapper;
using Common.Objects;
using DataAccessLayer.Objects.Inventory;
using MvcApp.areas.Inventory.Models;
using Repository.Interfaces;

namespace MvcApp.Services
{
    public interface IInventoryService
    {
        // Item 
        Task<List<ItemModel>> GetItem();
        Task<ItemModel> GetItem(int itemId);
        Task<ApiResult<bool>> InsertItem(ItemModel itemModel);
        Task<ApiResult<bool>> UpdateItem(ItemModel itemModel);
        Task<ApiResult<bool>> DeleteItem(int itemId);
        // Item Category
        Task<List<ItemCategoryModel>> GetItemCategory();
        Task<ItemCategoryModel> GetItemCategory(int itemCategoryId);
        Task<ApiResult<bool>> InsertItemCategory(ItemCategoryModel itemCategoryModel);
        Task<ApiResult<bool>> UpdateItemCategory(ItemCategoryModel itemCategoryModel);
        Task<ApiResult<bool>> DeleteItemCategory(int itemCategoryId);
        // Item Location
        Task<List<ItemLocationModel>> GetItemLocation();
        Task<ItemLocationModel> GetItemLocation(int itemLocationId);
        Task<ApiResult<bool>> InsertItemLocation(ItemLocationModel itemLocationModel);
        Task<ApiResult<bool>> UpdateItemLocation(ItemLocationModel itemLocationModel);
        Task<ApiResult<bool>> DeleteItemLocation(int itemLocationId);

        // Item Status
        Task<List<ItemStatusModel>> GetItemStatus();
        Task<ItemStatusModel> GetItemStatus(int itemStatusId);
        Task<ApiResult<bool>> InsertItemStatus(ItemStatusModel itemStatusModel);
        Task<ApiResult<bool>> UpdateItemStatus(ItemStatusModel itemStatusModel);
        Task<ApiResult<bool>> DeleteItemStatus(int itemStatusId);
    }

    public class InventoryService(IInventoryRepoService repo, IMapper mapper) : IInventoryService
    {
        public readonly IInventoryRepoService _Repo = repo;
        public readonly IMapper _Mapper = mapper;

        // Item
        public async Task<List<ItemModel>> GetItem()
        {
            var data = (await _Repo.InventoryItemGet()).Value;
            var result = _Mapper.Map<List<ItemModel>>(data);
            return result;
        }
        public async Task<ItemModel> GetItem(int itemId)
        {
            var data = (await _Repo.InventoryItemGet(itemId)).Value;
            var result = _Mapper.Map<ItemModel>(data);
            return result;
        }
        public async Task<ApiResult<bool>> InsertItem(ItemModel itemModel)
        {
            var data = await _Repo.InventoryItemInsert(_Mapper.Map<ItemApo>(itemModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> UpdateItem(ItemModel itemModel)
        {
            var data = await _Repo.InventoryItemUpdate(_Mapper.Map<ItemApo>(itemModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> DeleteItem(int itemId)
        {
            var data = await _Repo.InventoryItemDelete(itemId);
            var result = data;
            return result;
        }

        // Item Category
        public async Task<List<ItemCategoryModel>> GetItemCategory()
        {
            var data = (await _Repo.InventoryItemCategoryGet()).Value;
            var result = _Mapper.Map<List<ItemCategoryModel>>(data);
            return result;
        }
        public async Task<ItemCategoryModel> GetItemCategory(int itemCategoryId)
        {
            var data = (await _Repo.InventoryItemCategoryGet(itemCategoryId)).Value;
            var result = _Mapper.Map<ItemCategoryModel>(data);
            return result;
        }
        public async Task<ApiResult<bool>> InsertItemCategory(ItemCategoryModel itemCategoryModel)
        {
            var data = await _Repo.InventoryItemCategoryInsert(_Mapper.Map<ItemCategoryApo>(itemCategoryModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> UpdateItemCategory(ItemCategoryModel itemCategoryModel)
        {
            var data = await _Repo.InventoryItemCategoryUpdate(_Mapper.Map<ItemCategoryApo>(itemCategoryModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> DeleteItemCategory(int itemCategoryId)
        {
            var data = await _Repo.InventoryItemCategoryDelete(itemCategoryId);
            var result = data;
            return result;
        }

        // Item Location
        public async Task<List<ItemLocationModel>> GetItemLocation()
        {
            var data = (await _Repo.InventoryItemLocationGet()).Value;
            var result = _Mapper.Map<List<ItemLocationModel>>(data);
            return result;
        }
        public async Task<ItemLocationModel> GetItemLocation(int itemLocationId)
        {
            var data = (await _Repo.InventoryItemLocationGet(itemLocationId)).Value;
            var result = _Mapper.Map<ItemLocationModel>(data);
            return result;
        }
        public async Task<ApiResult<bool>> InsertItemLocation(ItemLocationModel itemLocationModel)
        {
            var data = await _Repo.InventoryItemLocationInsert(_Mapper.Map<ItemLocationApo>(itemLocationModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> UpdateItemLocation(ItemLocationModel itemLocationModel)
        {
            var data = await _Repo.InventoryItemLocationUpdate(_Mapper.Map<ItemLocationApo>(itemLocationModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> DeleteItemLocation(int itemLocationId)
        {
            var data = await _Repo.InventoryItemLocationDelete(itemLocationId);
            var result = data;
            return result;
        }

        // Item Status
        public async Task<List<ItemStatusModel>> GetItemStatus()
        {
            var data = (await _Repo.InventoryItemStatusGet()).Value;
            var result = _Mapper.Map<List<ItemStatusModel>>(data);
            return result;
        }
        public async Task<ItemStatusModel> GetItemStatus(int itemStatusId)
        {
            var data = (await _Repo.InventoryItemStatusGet(itemStatusId)).Value;
            var result = _Mapper.Map<ItemStatusModel>(data);
            return result;
        }
        public async Task<ApiResult<bool>> InsertItemStatus(ItemStatusModel itemStatusModel)
        {
            var data = await _Repo.InventoryItemStatusInsert(_Mapper.Map<ItemStatusApo>(itemStatusModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> UpdateItemStatus(ItemStatusModel itemStatusModel)
        {
            var data = await _Repo.InventoryItemStatusUpdate(_Mapper.Map<ItemStatusApo>(itemStatusModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> DeleteItemStatus(int itemStatusId)
        {
            var data = await _Repo.InventoryItemStatusDelete(itemStatusId);
            var result = data;
            return result;
        }
    }
}
