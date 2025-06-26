using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

public class GuestUserMiddleware
{
    private readonly RequestDelegate _next;

    public GuestUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            var claims = new List<Claim>
            {
                new Claim("Userid", "0"),
                new Claim("Username", "Guest"),
                new Claim("email", ""),
                new Claim(ClaimTypes.Role, "Guest"),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = false
            });
        }

        await _next(context);
    }
}
