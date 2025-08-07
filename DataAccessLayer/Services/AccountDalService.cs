using Common;
using Common.Objects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Objects.Inventory;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer.Services
{
    internal class AccountDalService(ConnectionOptions connectionOptions) : DataAccessService(connectionOptions), IAccountDalService
    {
        // User
        public async Task<ApiResult<List<UserApo>>> UserGet()
        {
            SqlParameter[] parameters =
            [
                new("@UserId", SqlDbType.Int) { Value = 0 }
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
                new("@UserId", userId)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserId"] = "UserId",
                ["UserName"] = "UserName",
                ["UserEmail"] = "UserEmail",
                ["UserHash"] = "UserHash",
                ["UserSalt"] = "UserSalt",
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
                new("@UserName", userName)
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
                var isDBSuccessful = await DeleteSqlAsync("Account.SpUserVaultDeleteSoft", parameters);

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
        public async Task<ApiResult<bool>> UserActivate(int userId)
        {
            SqlParameter[] parameters =
            [
                new("@UserId", userId)
            ];

            try
            {
                var isDBSuccessful = await ActivateSqlAsync("Account.SpUserVaultActivate", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Activated"
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

        // User Role
        public async Task<ApiResult<List<UserRoleApo>>> UserRoleGet()
        {
            SqlParameter[] parameters =
            [
                new("@UserRoleId", SqlDbType.Int) { Value = 0 }
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserRoleId"] = "UserRoleId",
                ["UserRoleName"] = "UserRoleName",
                ["IsActive"] = "IsActive"
            };

            try
            {
                var userRoles = await GetSqlListAsync<UserRoleApo>("Account.SpUserRoleGet", parameters, propertyMap);

                return new ApiResult<List<UserRoleApo>>
                {
                    IsSuccessful = true,
                    Value = userRoles,
                    Message = "Objects Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<UserRoleApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<UserRoleApo>> UserRoleGet(int userRoleId)
        {
            SqlParameter[] parameters =
            [
                new("@UserRoleId", userRoleId)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserRoleId"] = "UserRoleId",
                ["UserRoleName"] = "UserRoleName",
                ["IsActive"] = "IsActive"
            };

            try
            {
                var userRole = await GetSqlSingleAsync<UserRoleApo>("Account.SpUserRoleGet", parameters, propertyMap);

                return new ApiResult<UserRoleApo>
                {
                    IsSuccessful = true,
                    Value = userRole,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserRoleApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserRoleInsert(UserRoleApo userRoleApo)
        {
            SqlParameter[] parameters =
            [
                new("@UserRoleName", userRoleApo.UserRoleName)
            ];

            try
            {
                var isDBSuccessful = await InsertSqlAsync("Account.SpUserRoleInsert", parameters);

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
        public async Task<ApiResult<bool>> UserRoleUpdate(UserRoleApo userRoleApo)
        {
            SqlParameter[] parameters =
            [
                new("@UserRoleId", userRoleApo.UserRoleId),
                new("@UserRoleName", userRoleApo.UserRoleName)
            ];

            try
            {
                var isDBSuccessful = await UpdateSqlAsync("Account.SpUserRoleUpdate", parameters);

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
        public async Task<ApiResult<bool>> UserRoleDelete(int userRoleId)
        {
            SqlParameter[] parameters =
            [
                new("@UserRoleId", userRoleId)
            ];

            try
            {
                var isDBSuccessful = await DeleteSqlAsync("Account.SpUserRoleDeleteSoft", parameters);

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
        public async Task<ApiResult<bool>> UserRoleActivate(int userRoleId)
        {
            SqlParameter[] parameters =
            [
                new("@UserRoleId", userRoleId)
            ];

            try
            {
                var isDBSuccessful = await ActivateSqlAsync("Account.SpUserRoleActivate", parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = isDBSuccessful,
                    Message = $"Object Activated"
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

        // User N User Role
        public async Task<ApiResult<List<UserRoleApo>>> UserNUserRoleGet(int userId)
        {
            // PASS UserId first, then zero out UserRoleId
            SqlParameter[] parameters =
            [
                new("@UserId",     userId),
                new("@UserRoleId", SqlDbType.Int) { Value = 0 }
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserRoleId"] = "UserRoleId",
                ["UserRoleName"] = "UserRoleName"
            };

            try
            {
                var users = await GetSqlListAsync<UserRoleApo>("Account.SpUserVaultNRoleGet", parameters, propertyMap);

                return new ApiResult<List<UserRoleApo>>()
                {
                    IsSuccessful = true,
                    Value = users,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<UserRoleApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<List<UserRoleApo>>> UserNUserRoleGet(int userId, int userRoleId)
        {
            SqlParameter[] parameters =
            [
                new("@UserId",     userId),
                new("@UserRoleId", userRoleId)
            ];

            Dictionary<string, string> propertyMap = new()
            {
                // [C# Property] = DB Column
                ["UserRoleId"] = "UserRoleId",
                ["UserRoleName"] = "UserRoleName"
            };

            try
            {
                var users = await GetSqlListAsync<UserRoleApo>("Account.SpUserVaultNRoleGet", parameters, propertyMap);

                return new ApiResult<List<UserRoleApo>>()
                {
                    IsSuccessful = true,
                    Value = users,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<UserRoleApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserNUserRoleInsert(int userId, int UserRoleId)
        {
            if (userId == 0 || UserRoleId == 0)
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Ids must be positive"
                };

            var parameters = new[]
            {
                new SqlParameter("@UserId",     userId),
                new SqlParameter("@UserRoleId", UserRoleId)
            };

            try
            {
                var isDBSuccessful = await InsertSqlAsync(
                    "Account.SpUserVaultNRoleInsert",
                    parameters);

                return new ApiResult<bool>
                {
                    IsSuccessful = isDBSuccessful,
                    Value = isDBSuccessful,
                    Message = isDBSuccessful
                                     ? "Role assigned"
                                     : "No rows inserted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"DB Error: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserNUserRoleDelete(int userId, int UserRoleId)
        {
            SqlParameter[] parameters =
            [
                new("@UserId", userId),
                new("@UserRoleId", UserRoleId)
            ];

            try
            {
                var isDBSuccessful = await DeleteSqlAsync("Account.SpUserVaultNRoleDelete", parameters);

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
