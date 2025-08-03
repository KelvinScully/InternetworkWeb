using Common.Objects;
using DataAccessLayer.Objects.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    internal interface IInventoryBllService
    {
        Task<ApiResult<ItemStatusApo>> GetItemStatus(int itemStatusId);
        Task<ApiResult<List<ItemStatusApo>>> GetItemStatus();
    }
}
