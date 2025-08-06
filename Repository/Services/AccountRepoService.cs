using BusinessLogicLayer.Interfaces;
using Common;
using Common.Objects;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Objects.Inventory;
using Repository.Interfaces;
using System.Net.Http;

namespace Repository.Services
{
    internal class AccountRepoService(ConnectionOptions options, IAccountBllService bll) : RepositoryBase(options), IAccountRepoService
    {
        private readonly IAccountBllService _Bll = bll;

        public async Task<ApiResult<List<UserApo>>> AccountGetUser(bool returnInactive)
        {
            try
            {
                var result = await _Bll.UserGet();
                if (result == null || !result.IsSuccessful || result.Value == null || result.Value.Count == 0)
                {
                    return new ApiResult<List<UserApo>>
                    {
                        IsSuccessful = false,
                        Value = [],
                        Message = "Empty result"
                    };
                }
                else
                {
                    if (!returnInactive)
                        result.Value = result.Value.Where(obj => obj.IsActive).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<List<UserApo>>
                {
                    IsSuccessful = false,
                    Value = [],
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> AccountGetUser(int userId)
        {
            try
            {
                var result = await _Bll.UserGet(userId);
                if (result == null || !result.IsSuccessful || result.Value == null)
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = "Empty result"
                    };
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> AccountGetUser(string userName)
        {
            try
            {
                var result = await _Bll.UserGet(userName);
                return result ?? new ApiResult<UserApo> { IsSuccessful = false, Value = new(), Message = "Empty result" };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> AccountUserInsert(UserApo userApo)
        {
            try
            {
                var result = await _Bll.UserInsert(userApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"User Inserted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"User Insert Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> AccountUserUpdate(UserApo userApo)
        {
            try
            {
                var result = await _Bll.UserUpdate(userApo);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"User Updated"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"User Update Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> AccountUserVerify(int userId)
        {
            try
            {
                var result = await _Bll.UserVerify(userId);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"User Verified"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"User Verify Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<bool>> AccountUserDelete(int userId)
        {
            try
            {
                var result = await _Bll.UserDelete(userId);
                if (result.IsSuccessful)
                {
                    return new ApiResult<bool>
                    {
                        IsSuccessful = true,
                        Value = result.IsSuccessful,
                        Message = $"User Deleted"
                    };
                }
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = result.IsSuccessful,
                    Message = $"User Delete Failed"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<ApiResult<UserApo>> Register(UserApo user)
        {
            try
            {
                var result = await _Bll.Register(user);

                if (!result.IsSuccessful)
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"Failed: {result.Message}"
                    };
                }

                return result ?? new ApiResult<UserApo> { IsSuccessful = false, Value = new(), Message = "Empty result" };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
        public async Task<ApiResult<UserApo>> Authenticate(UserApo user)
        {
            try
            {
                var result = await _Bll.Authenticate(user);

                if (!result.IsSuccessful)
                {
                    return new ApiResult<UserApo>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"Failed: {result.Message}"
                    };
                }

                return result ?? new ApiResult<UserApo> { IsSuccessful = false, Value = new(), Message = "Empty result" };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserApo>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
    }
}
