using BusinessLogicLayer.Interfaces;
using Common.Objects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Objects.Inventory;
using System.Security.Cryptography;

namespace BusinessLogicLayer.Services
{
    internal class AccountBllService(IAccountDalService dal) : IAccountBllService
    {
        private readonly IAccountDalService _Dal = dal;

        // User
        public async Task<ApiResult<List<UserApo>>> UserGet()
        {
            try
            {
                var dalResult = await _Dal.UserGet();

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<List<UserApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                foreach (var user in dalResult.Value)
                {
                    user.UserRoles = (await _Dal.UserNUserRoleGet(user.UserId)).Value;
                }

                return new ApiResult<List<UserApo>>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Objects Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<UserApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> UserGet(int userId)
        {
            if (userId == 0)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.UserGet(userId);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                dalResult.Value.UserRoles = (await _Dal.UserNUserRoleGet(dalResult.Value.UserId)).Value;

                return new ApiResult<UserApo>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> UserGet(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = "Username is null or Empty"
                };
            }

            try
            {
                var dalResult = await _Dal.UserGet(userName);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<UserApo>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }

        public async Task<ApiResult<bool>> UserInsert(UserApo userApo)
        {
            try
            {
                // 128 Bit Salt
                byte[] userSalt = RandomNumberGenerator.GetBytes(16);
                // 256 Bit Hash
                using var rfc2898 = new Rfc2898DeriveBytes(userApo.Password, userSalt, 100_000, HashAlgorithmName.SHA256);
                byte[] userHash = rfc2898.GetBytes(32);

                var dalResult = await _Dal.UserInsert(new UserApo() { UserName = userApo.UserName, UserEmail = userApo.UserEmail, UserHash = userHash, UserSalt = userSalt });

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserUpdate(UserApo userApo)
        {
            try
            {
                var dalResult = await _Dal.UserUpdate(userApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserVerify(int userId)
        {
            if (userId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.UserVerify(userId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Verified"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserDelete(int userId)
        {
            if (userId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.UserDelete(userId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserActivate(int userId)
        {
            if (userId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.UserActivate(userId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Activated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }

        public async Task<ApiResult<UserApo>> Register(UserApo userApo)
        {
            if (string.IsNullOrEmpty(userApo.UserName) || string.IsNullOrEmpty(userApo.Password) || string.IsNullOrEmpty(userApo.UserEmail))
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new()
                    {
                        UserName = userApo.UserName,
                        UserEmail = userApo.UserEmail,
                    },
                    Message = "Username, password, or email is null or empty"
                };
            }

            try
            {
                // 128 Bit Salt
                byte[] userSalt = RandomNumberGenerator.GetBytes(16);
                // 256 Bit Hash
                using var rfc2898 = new Rfc2898DeriveBytes(userApo.Password, userSalt, 100_000, HashAlgorithmName.SHA256);
                byte[] userHash = rfc2898.GetBytes(32);

                var dalResult = await _Dal.UserInsert(new UserApo() { UserName = userApo.UserName, UserEmail = userApo.UserEmail, UserHash = userHash, UserSalt = userSalt });

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<UserApo>
                {
                    IsSuccessful = true,
                    Value = (await _Dal.UserGet(userApo.UserName)).Value,
                    Message = "User Registered"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> Authenticate(UserApo userApo)
        {
            if (string.IsNullOrEmpty(userApo.UserName) || string.IsNullOrEmpty(userApo.Password))
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new()
                    {
                        UserName = userApo.UserName,
                    },
                    Message = "Username or Password is null or empty"
                };
            }

            try
            {
                var dalHashNSalt = await _Dal.UserGetHashNSalt(userApo.UserName);
                if (!dalHashNSalt.IsSuccessful)
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalHashNSalt.Message}"
                    };
                }

                var dalHash = dalHashNSalt.Value.UserHash;
                var dalSalt = dalHashNSalt.Value.UserSalt;

                // 256 Bit Hash
                using var rfc2898 = new Rfc2898DeriveBytes(userApo.Password, dalSalt, 100_000, HashAlgorithmName.SHA256);
                byte[] userHash = rfc2898.GetBytes(32);


                if (!userHash.SequenceEqual(dalHash))
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        // Only the password is wrong but we send this
                        Message = $"Username or Password is wrong"
                    };
                }

                // Now we get the User Object
                var dalResult = await _Dal.UserGet(userApo.UserName);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                dalResult.Value.UserRoles = (await _Dal.UserNUserRoleGet(dalResult.Value.UserId)).Value;

                return new ApiResult<UserApo>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "User Authenticated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }

        // User Role
        public async Task<ApiResult<List<UserRoleApo>>> UserRoleGet()
        {
            try
            {
                var dalResult = await _Dal.UserRoleGet();

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<List<UserRoleApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<List<UserRoleApo>>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Objects Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<UserRoleApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<UserRoleApo>> UserRoleGet(int userRoleId)
        {
            if (userRoleId == 0)
            {
                return new ApiResult<UserRoleApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.UserRoleGet(userRoleId);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<UserRoleApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<UserRoleApo>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserRoleApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserRoleInsert(UserRoleApo userRoleApo)
        {
            try
            {
                var dalResult = await _Dal.UserRoleInsert(userRoleApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserRoleUpdate(UserRoleApo userRoleApo)
        {
            try
            {
                var dalResult = await _Dal.UserRoleUpdate(userRoleApo);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserRoleDelete(int userRoleId)
        {
            if (userRoleId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.UserRoleDelete(userRoleId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserRoleActivate(int userRoleId)
        {
            if (userRoleId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.UserRoleActivate(userRoleId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Activated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }

        // User N User Role
        public async Task<ApiResult<bool>> UserNUserRoleInsert(int userId, int userRoleId)
        {
            if (userId == 0 || userRoleId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "An Id is equal to 0. Id must be a positive number"
                };
            }
            try
            {
                var dalResult = await _Dal.UserNUserRoleInsert(userId, userRoleId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Updated"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> UserNUserRoleDelete(int userId, int userRoleId)
        {
            if (userId == 0 || userRoleId == 0)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "An Id is equal to 0. Id must be a positive number"
                };
            }

            try
            {
                var dalResult = await _Dal.UserNUserRoleDelete(userId, userRoleId);

                if (!dalResult.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "Object Deleted"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
    }
}
