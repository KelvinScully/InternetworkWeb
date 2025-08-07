using Common.Objects;
using DataAccessLayer.Objects.Account;

namespace Repository.Interfaces
{
    public interface IAccountRepoService
    {
        // User
        Task<ApiResult<List<UserApo>>> AccountUserGet(bool returnInactive);
        Task<ApiResult<UserApo>> AccountUserGet(int userId);
        Task<ApiResult<UserApo>> AccountUserGet(string userName);
        Task<ApiResult<bool>> AccountUserInsert(UserApo userApo);
        Task<ApiResult<bool>> AccountUserUpdate(UserApo userApo);
        Task<ApiResult<bool>> AccountUserVerify(int userId);
        Task<ApiResult<bool>> AccountUserDelete(int userId);
        Task<ApiResult<bool>> AccountUserActivate(int userId);

        Task<ApiResult<UserApo>> Register(UserApo user);
        Task<ApiResult<UserApo>> Authenticate(UserApo user);

        // User Role
        Task<ApiResult<List<UserRoleApo>>> AccountUserRoleGet(bool returnInactive);
        Task<ApiResult<UserRoleApo>> AccountUserRoleGet(int userRoleId);
        Task<ApiResult<bool>> AccountUserRoleInsert(UserRoleApo userRoleApo);
        Task<ApiResult<bool>> AccountUserRoleUpdate(UserRoleApo userRoleApo);
        Task<ApiResult<bool>> AccountUserRoleDelete(int userRoleId);
        Task<ApiResult<bool>> AccountUserRoleActivate(int userRoleId);

        // User N User Role
        Task<ApiResult<bool>> AccountUserNUserRoleInsert(int userId, int userRoleId);
        Task<ApiResult<bool>> AccountUserNUserRoleDelete(int userId, int userRoleId);
    }
}
