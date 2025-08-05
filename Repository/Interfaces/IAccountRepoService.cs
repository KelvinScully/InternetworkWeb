using Common.Objects;
using DataAccessLayer.Objects.Account;

namespace Repository.Interfaces
{
    public interface IAccountRepoService
    {
        Task<ApiResult<List<UserApo>>> AccountGetUser();
        Task<ApiResult<UserApo>> AccountGetUser(int userId);
        Task<ApiResult<UserApo>> AccountGetUser(string userName);

        Task<ApiResult<UserApo>> Register(UserApo user);
        Task<ApiResult<UserApo>> Authenticate(UserApo user);

        Task<ApiResult<bool>> AccountUserInsert(UserApo userApo);
        Task<ApiResult<bool>> AccountUserUpdate(UserApo userApo);
        Task<ApiResult<bool>> AccountUserVerify(int userId);
        Task<ApiResult<bool>> AccountUserDelete(int userId);
    }
}
