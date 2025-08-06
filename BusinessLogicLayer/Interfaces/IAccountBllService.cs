using Common.Objects;
using DataAccessLayer.Objects.Account;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAccountBllService
    {
        // User
        Task<ApiResult<List<UserApo>>> UserGet();
        Task<ApiResult<UserApo>> UserGet(int userId);
        Task<ApiResult<UserApo>> UserGet(string userName);
        Task<ApiResult<bool>> UserInsert(UserApo userApo);
        Task<ApiResult<bool>> UserUpdate(UserApo userApo);
        Task<ApiResult<bool>> UserVerify(int userId);
        Task<ApiResult<bool>> UserDelete(int userId);

        Task<ApiResult<UserApo>> Register(UserApo user);
        Task<ApiResult<UserApo>> Authenticate(UserApo user);

        // User Role
        Task<ApiResult<List<UserRoleApo>>> UserRoleGet();
        Task<ApiResult<UserRoleApo>> UserRoleGet(int userRoleId);
        Task<ApiResult<bool>> UserRoleInsert(UserRoleApo userRoleApo);
        Task<ApiResult<bool>> UserRoleUpdate(UserRoleApo userRoleApo);
        Task<ApiResult<bool>> UserRoleDelete(int userRoleId);

        // User N User Role
        Task<ApiResult<bool>> UserNUserRoleInsert(int userId, int userRoleId);
        Task<ApiResult<bool>> UserNUserRoleDelete(int userId, int userRoleId);
    }
}
