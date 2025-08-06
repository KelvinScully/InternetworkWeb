using Common;
using Common.Objects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Objects.Inventory;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Services
{
    internal class AccountDalService(ConnectionOptions connectionOptions) : DataAccessService(connectionOptions), IAccountDalService
    {
        public async Task<ApiResult<List<UserApo>>> UserGet()
        {
            SqlParameter[] parameters =
            [
                new("@UserId", 0),
                new("@IsActive", 1)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserId"] = "UserId",
                ["UserName"] = "UserName",
                ["UserEmail"] = "UserEmail",
                ["IsEmailVerified"] = "IsEmailVerified",
                ["IsActive"] = "IsActive"
            };

            try
            {
                var users = await GetSqlListAsync<UserApo>("Account.SpUserVaultGetById", parameters, propertyMap);

                return new ApiResult<List<UserApo>>()
                {
                    IsSuccessful = true,
                    Value = users,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<UserApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> UserGet(int userId)
        {
            SqlParameter[] parameters =
            [
                new("@UserId", userId),
                new("@IsActive", 1)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserId"] = "UserId",
                ["UserName"] = "UserName",
                ["UserEmail"] = "UserEmail",
                ["IsEmailVerified"] = "IsEmailVerified",
                ["IsActive"] = "IsActive"
            };

            try
            {
                var user = await GetSqlSingleAsync<UserApo>("Account.SpUserVaultGetById", parameters, propertyMap);

                return new ApiResult<UserApo>
                {
                    IsSuccessful = true,
                    Value = user,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> UserGet(string userName)
        {
            SqlParameter[] parameters =
            [
                new("@UserName", userName),
                new("@IsActive", 1)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserId"] = "UserId",
                ["UserName"] = "UserName",
                ["UserEmail"] = "UserEmail",
                ["IsEmailVerified"] = "IsEmailVerified",
                ["IsActive"] = "IsActive"
            };

            try
            {
                var user = await GetSqlSingleAsync<UserApo>("Account.SpUserVaultGetByUserName", parameters, propertyMap);

                return new ApiResult<UserApo>
                {
                    IsSuccessful = true,
                    Value = user,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> UserGetHashNSalt(string userName)
        {
            SqlParameter[] parameters =
            [
                new("@UserName", userName)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserHash"] = "UserHash",
                ["UserSalt"] = "UserSalt"
            };

            try
            {
                var user = await GetSqlSingleAsync<UserApo>("Account.SpUserVaultGetHashNSalt", parameters, propertyMap);

                return new ApiResult<UserApo>
                {
                    IsSuccessful = true,
                    Value = user,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserInsert(UserApo userApo)
        {
            SqlParameter[] parameters =
            [
                new("@UserName", userApo.UserName),
                new("@UserEmail", userApo.UserEmail),
                new("@UserHash", userApo.UserHash),
                new("@UserSalt", userApo.UserSalt)
            ];

            try
            {
                var isDBSuccessful = await InsertSqlAsync("Account.SpUserVaultInsert", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserUpdate(UserApo userApo)
        {
            SqlParameter[] parameters =
            [
                new("@UserId", userApo.UserId),
                new("@UserName", userApo.UserName),
                new("@UserEmail", userApo.UserEmail),
                new("@UserHash", userApo.UserHash),
                new("@UserSalt", userApo.UserSalt)
            ];

            try
            {
                var isDBSuccessful = await UpdateSqlAsync("Account.SpUserVaultUpdate", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Update"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserVerify(int userId)
        {
            SqlParameter[] parameters =
            [
                new("@UserId", userId)
            ];

            try
            {
                var isDBSuccessful = await UpdateSqlAsync("Account.SpUserVaultVerify", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Verified"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserDelete(int userId)
        {
            SqlParameter[] parameters =
            [
                new("@UserId", userId)
            ];

            try
            {
                var isDBSuccessful = await DeleteSqlAsync("Account.SpUserVaultDelete", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
    }
}
