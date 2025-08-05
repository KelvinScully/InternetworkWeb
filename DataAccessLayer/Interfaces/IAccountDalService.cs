using Common.Objects;
using DataAccessLayer.Objects.Account;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountDalService
    {
        Task<ApiResult<List<UserApo>>> UserGet();
        Task<ApiResult<UserApo>> UserGet(int userId);
        Task<ApiResult<UserApo>> UserGet(string userName);
        Task<ApiResult<UserApo>> UserGetHashNSalt(string userName);
        Task<ApiResult<bool>> UserInsert(UserApo userApo);
        Task<ApiResult<bool>> UserUpdate(UserApo userApo);
        Task<ApiResult<bool>> UserVerify(int userId);
        Task<ApiResult<bool>> UserDelete(int userId);
    }
}
