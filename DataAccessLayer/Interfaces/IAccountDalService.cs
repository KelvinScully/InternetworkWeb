using Common.Objects;
using DataAccessLayer.Objects.Account;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountDalService
    {
        // User
        Task<ApiResult<List<UserApo>>> UserGet();
        Task<ApiResult<UserApo>> UserGet(int userId);
        Task<ApiResult<UserApo>> UserGet(string userName);
        Task<ApiResult<UserApo>> UserGetHashNSalt(string userName);
        Task<ApiResult<bool>> UserInsert(UserApo userApo);
        Task<ApiResult<bool>> UserUpdate(UserApo userApo);
        Task<ApiResult<bool>> UserVerify(int userId);
        Task<ApiResult<bool>> UserDelete(int userId);
        Task<ApiResult<bool>> UserActivate(int userId);

        // User Role
        Task<ApiResult<List<UserRoleApo>>> UserRoleGet();
        Task<ApiResult<UserRoleApo>> UserRoleGet(int userRoleId);
        Task<ApiResult<bool>> UserRoleInsert(UserRoleApo userRoleApo);
        Task<ApiResult<bool>> UserRoleUpdate(UserRoleApo userRoleApo);
        Task<ApiResult<bool>> UserRoleDelete(int userRoleId);
        Task<ApiResult<bool>> UserRoleActivate(int userRoleId);

        // User N User Role
        Task<ApiResult<List<UserRoleApo>>> UserNUserRoleGet(int userId);
        Task<ApiResult<List<UserRoleApo>>> UserNUserRoleGet(int userId, int userRoleId);
        Task<ApiResult<bool>> UserNUserRoleInsert(int userId, int UserRoleId);
        Task<ApiResult<bool>> UserNUserRoleDelete(int userId, int UserRoleId);
    }
}
