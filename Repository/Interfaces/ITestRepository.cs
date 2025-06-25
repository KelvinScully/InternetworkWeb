using ACommon.Objects;
using Repository.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITestRepository
    {
        Task<ApiResult<TestItem>> TestItemGet(int testItemId);
    }
}
