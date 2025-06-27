using ACommon.Objects;
using Common;
using Repository.Interfaces;
using Repository.Objects;
using Repository.Objects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    internal class AccountRepository: RepositoryBase, IAccountRepository
    {
        public AccountRepository(ConnectionOptions options) : base(options)
        { }

        public async Task<ApiResult<User>> ApiRegister(Register user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/V1/Account/Register", user);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResult<User>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"Failed with status code: {response.StatusCode}"
                    };
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResult<User>>();
                return result ?? new ApiResult<User> { IsSuccessful = false, Value = new(), Message = "Empty result" };
            }
            catch (Exception ex)
            {
                return new ApiResult<User>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<ApiResult<User>> ApiLogin(Authenticate user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/V1/Account/Login", user);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResult<User>
                    {
                        IsSuccessful = false,
                        Value = new(),
                        Message = $"Failed with status code: {response.StatusCode}"
                    };
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResult<User>>();
                return result ?? new ApiResult<User> { IsSuccessful = false, Value = new(), Message = "Empty result" };
            }
            catch (Exception ex)
            {
                return new ApiResult<User>
                {
                    IsSuccessful = false,
                    Value = new(),
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
    }
}
