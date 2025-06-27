using Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcApp.Services;
using Repository;

namespace MvcApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Access config settings
            var config = builder.Configuration;
            var connectionSection = config.GetSection("ApiSettings");

            // Pull UseLocalApi toggle
            bool useLocalApi = connectionSection.GetValue<bool>("UseLocalApi");

            // Pull correct connection string
            string connectionString = useLocalApi ? connectionSection.GetValue<string>("LocalApi") : connectionSection.GetValue<string>("PublishedApi");
            bool isIISExpress = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES")?.Contains("IIS") ?? false;


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton(new ConnectionOptions
            {
                ConnectionString = connectionString,
                IsLocal = useLocalApi,
                IsIISExpress = isIISExpress
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Gate";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthorization();

            builder.Services.AddRepository();
            builder.Services.AddScoped<AccountService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseMiddleware<GuestUserMiddleware>();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Account}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Inventory}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
