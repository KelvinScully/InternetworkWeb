using AutoMapper;
using MvcApp.areas.Inventory.Models;
using Repository.Interfaces;

namespace MvcApp.Services
{
    public interface IInventoryService
    {
        Task<List<ItemStatusModel>> GetitemStatus();
        Task<ItemStatusModel> GetitemStatus(int itemStatusId);
    }

    public class InventoryService(IInventoryRepoService repo, IMapper mapper) : IInventoryService
    {
        public readonly IInventoryRepoService _Repo = repo;
        public readonly IMapper _Mapper = mapper;

        public async Task<List<ItemStatusModel>> GetitemStatus()
        {
            var data = (await _Repo.InventoryGetItemStatus()).Value;
            var result = _Mapper.Map<List<ItemStatusModel>>(data);
            return result;
        }
        public async Task<ItemStatusModel> GetitemStatus(int itemStatusId)
        {
            var data = (await _Repo.InventoryGetItemStatus(itemStatusId)).Value;
            var result = _Mapper.Map<ItemStatusModel>(data);
            return result;
        }
    }
}
