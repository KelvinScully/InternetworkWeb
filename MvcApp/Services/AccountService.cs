using AutoMapper;
using Common.Objects;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Objects.Inventory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MvcApp.Areas.Account.Models;
using MvcApp.Areas.Inventory.Models;
using Repository.Interfaces;
using System.Security.Claims;

namespace MvcApp.Services
{
    public interface IAccountService
    {
        // User
        Task<List<UserModel>> GetUser(bool returnInactive);
        Task<UserModel> GetUser(int userId);
        Task<UserModel> GetUser(string userName);
        Task<ApiResult<bool>> InsertUser(UserModel userModel);
        Task<ApiResult<bool>> UpdateUser(UserModel userModel);
        Task<ApiResult<bool>> VerifyUser(int userId);
        Task<ApiResult<bool>> DeleteUser(int userId);

        Task<UserModel> Register(LoginRegisterModel user);
        Task<UserModel> Authenticate(LoginRegisterModel user);

        // User Role
        Task<List<UserRoleModel>> GetUserRole(bool returnInactive);
        Task<UserRoleModel> GetUserRole(int userRoleId);
        Task<ApiResult<bool>> InsertUserRole(UserRoleModel userRoleModel);
        Task<ApiResult<bool>> UpdateUserRole(UserRoleModel userRoleModel);
        Task<ApiResult<bool>> DeleteUserRole(int userRoleId);

        // User N User Role
        Task<ApiResult<bool>> InsertUserNUserRole(int userId, int userRoleId);
        Task<ApiResult<bool>> DeleteUserNUserRole(int userId, int userRoleId);

        Task SignOutUserAsync();
    }
    public class AccountService(IAccountRepoService repo, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IAccountService
    {
        public readonly IAccountRepoService _Repo = repo;
        public readonly IMapper _Mapper = mapper;
        public readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<List<UserModel>> GetUser(bool returnInactive)
        {
            var data = (await _Repo.AccountUserGet(returnInactive)).Value;
            var result = _Mapper.Map<List<UserModel>>(data);
            foreach (var obj in result)
                obj.ShowInactive = returnInactive;
            return result;
        }
        public async Task<UserModel> GetUser(int userId)
        {
            var data = (await _Repo.AccountUserGet(userId)).Value;
            var result = _Mapper.Map<UserModel>(data);
            return result;
        }
        public async Task<UserModel> GetUser(string userName)
        {
            var data = (await _Repo.AccountUserGet(userName)).Value;
            var result = _Mapper.Map<UserModel>(data);
            return result;
        }
        public async Task<ApiResult<bool>> InsertUser(UserModel userModel)
        {
            var data = await _Repo.AccountUserInsert(_Mapper.Map<UserApo>(userModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> UpdateUser(UserModel userModel)
        {
            // 1) Grab the current, fully hydrated user from the repo
            var existingResult = await _Repo.AccountUserGet(userModel.UserId);
            if (!existingResult.IsSuccessful)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = "User not found"
                };
            }

            var userToUpdate = existingResult.Value!;   // this has the real UserHash, UserSalt, flags, etc.

            // 2) Overwrite only what you mean to change
            userToUpdate.UserName = userModel.UserName;
            userToUpdate.UserEmail = userModel.UserEmail;

            // 3) Call update—now the DAL sees a complete object, and your stored proc can happily
            //    check duplicates (skipping this record) and update just the name & email.
            var updateResult = await _Repo.AccountUserUpdate(userToUpdate);
            return updateResult;
        }
        public async Task<ApiResult<bool>> VerifyUser(int userId)
        {
            var data = await _Repo.AccountUserVerify(userId);
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> DeleteUser(int userId)
        {
            var data = await _Repo.AccountUserDelete(userId);
            var result = data;
            return result;
        }

        public async Task<UserModel> Register(LoginRegisterModel user)
        {
            var userModel = _Mapper.Map<UserModel>(user);
            var repoResult = await _Repo.Register(_Mapper.Map<UserApo>(userModel));
            var authenticatedUser = repoResult.IsSuccessful ? repoResult.Value! : new();

            if (!authenticatedUser.IsDefault())
                await SignInUserAsync(authenticatedUser, user.RememberMe);

            return _Mapper.Map<UserModel>(authenticatedUser);
        }
        public async Task<UserModel> Authenticate(LoginRegisterModel user)
        {
            var userModel = _Mapper.Map<UserModel>(user);
            var repoResult = await _Repo.Authenticate(_Mapper.Map<UserApo>(userModel));
            var authenticatedUser = repoResult.IsSuccessful ? repoResult.Value! : new();

            if (!authenticatedUser.IsDefault())
                await SignInUserAsync(authenticatedUser, user.RememberMe);

            return _Mapper.Map<UserModel>(authenticatedUser);
        }

        // User Role
        public async Task<List<UserRoleModel>> GetUserRole(bool returnInactive)
        {
            var data = (await _Repo.AccountUserRoleGet(returnInactive)).Value;
            var result = _Mapper.Map<List<UserRoleModel>>(data);
            foreach (var obj in result)
                obj.ShowInactive = returnInactive;
            return result;
        }
        public async Task<UserRoleModel> GetUserRole(int userRoleId)
        {
            var data = (await _Repo.AccountUserRoleGet(userRoleId)).Value;
            var result = _Mapper.Map<UserRoleModel>(data);
            return result;
        }
        public async Task<ApiResult<bool>> InsertUserRole(UserRoleModel userRoleModel)
        {
            var data = await _Repo.AccountUserRoleInsert(_Mapper.Map<UserRoleApo>(userRoleModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> UpdateUserRole(UserRoleModel userRoleModel)
        {
            var data = await _Repo.AccountUserRoleUpdate(_Mapper.Map<UserRoleApo>(userRoleModel));
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> DeleteUserRole(int userRoleId)
        {
            var data = await _Repo.AccountUserRoleDelete(userRoleId);
            var result = data;
            return result;
        }

        // User N User Role
        public async Task<ApiResult<bool>> InsertUserNUserRole(int userId, int userRoleId)
        {
            var data = await _Repo.AccountUserNUserRoleInsert(userId, userRoleId);
            var result = data;
            return result;
        }
        public async Task<ApiResult<bool>> DeleteUserNUserRole(int userId, int userRoleId)
        {
            var data = await _Repo.AccountUserNUserRoleDelete(userId, userRoleId);
            var result = data;
            return result;
        }

        // Service Methods
        private async Task SignInUserAsync(UserApo user, bool rememberMe)
        {
            var context = _httpContextAccessor.HttpContext!;
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.UserEmail)
            };

            foreach (var role in user.UserRoles)
                claims.Add(new Claim(ClaimTypes.Role, role.UserRoleName));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = rememberMe
            });
        }

        public async Task SignOutUserAsync()
        {
            var context = _httpContextAccessor.HttpContext!;
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = new List<Claim>
            {
                new Claim("Userid", "0"),
                new Claim("Username", "Guest"),
                new Claim("email", ""),
                new Claim(ClaimTypes.Role, "Guest"),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            context.User = principal;
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = false
            });
        }
    }
}
