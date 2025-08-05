using Common.Objects;
using DataAccessLayer.Objects.Account;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAccountBllService
    {
        Task<ApiResult<List<UserApo>>> UserGet();
        Task<ApiResult<UserApo>> UserGet(int userId);
        Task<ApiResult<UserApo>> UserGet(string userName);

        Task<ApiResult<UserApo>> Register(UserApo user);
        Task<ApiResult<UserApo>> Authenticate(UserApo user);

        Task<ApiResult<bool>> UserInsert(UserApo userApo);
        Task<ApiResult<bool>> UserUpdate(UserApo userApo);
        Task<ApiResult<bool>> UserVerify(int userId);
        Task<ApiResult<bool>> UserDelete(int userId);
    }
}
