using AutoMapper;
using DataAccessLayer.Objects.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MvcApp.Areas.Account.Model;
using Repository.Interfaces;
using System.Security.Claims;

namespace MvcApp.Services
{
    public interface IAccountService
    {
        Task<UserModel> Register(LoginRegisterModel user);
        Task<UserModel> Authenticate(LoginRegisterModel user);
    }
    public class AccountService(IAccountRepoService repo, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IAccountService
    {
        public readonly IAccountRepoService _Repo = repo;
        public readonly IMapper _Mapper = mapper;
        public readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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
    }
}
