using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MvcApp.areas.Account.Model;
using Repository.Interfaces;
using Repository.Objects.Account;
using System.Security.Claims;

namespace MvcApp.Services
{
    public class AccountService
    {
        private IAccountRepository _AccountRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _AccountRepo = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> Register(LoginRegisterModel user)
        {
            var repoResult = await _AccountRepo.ApiRegister(new Register()
            { Username = user.Username, UserPassword = user.Password, UserEmail = user.Email });

            var authenticatedUser = repoResult.IsSuccessful ? repoResult.Value! : new User();

            if (!authenticatedUser.IsDefault())
                await SignInUserAsync(authenticatedUser, user.RememberMe);

            return authenticatedUser;
        }
        public async Task<User> Login(LoginRegisterModel user)
        {
            var repoResult = await _AccountRepo.ApiLogin(new Authenticate()
            { Username = user.Username, UserPassword = user.Password });

            var authenticatedUser = repoResult.IsSuccessful ? repoResult.Value! : new User();

            if (!authenticatedUser.IsDefault())
                await SignInUserAsync(authenticatedUser, user.RememberMe);

            return authenticatedUser;
        }

        private async Task SignInUserAsync(User user, bool rememberMe)
        {
            var context = _httpContextAccessor.HttpContext!;
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.UserEmail)
            };

            foreach (var role in user.UserRoles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = rememberMe
            });
        }
    }
}
