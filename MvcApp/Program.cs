using AutoMapper;
using BusinessLogicLayer;
using Common;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging.Abstractions;
using MvcApp.AutoMapper;
using MvcApp.Services;
using Repository;

namespace MvcApp
{
    public static class MVCAppServiceRegistration
    {
        public static IServiceCollection AddMVCAppServices(this IServiceCollection services)
        {
            var configExpr = new MapperConfigurationExpression();
            configExpr.AddProfile<AutoMapperProfile>();
            var mappingConfig = new MapperConfiguration(configExpr, NullLoggerFactory.Instance);
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddScoped<Interface, Implementation>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IInventoryService, InventoryService>();
            return services;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Access config settings
            var config = builder.Configuration;
            var connectionSection = config.GetSection("DbSettings");

            // Pull UseLocalApi toggle
            bool useLocalDb = connectionSection.GetValue<bool>("UseLocalDb");

            // Pull correct connection string
            string connectionString = useLocalDb ? connectionSection.GetValue<string>("LocalDb") : connectionSection.GetValue<string>("PublishedDb");
            bool isIISExpress = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES")?.Contains("IIS") ?? false;


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton(new ConnectionOptions
            {
                ConnectionString = connectionString,
                IsLocal = useLocalDb,
                IsIISExpress = isIISExpress
            });

            builder.Services.AddDataAccessServices();
            builder.Services.AddBusinessLogicServices();
            builder.Services.AddRepositoryServices();
            builder.Services.AddMVCAppServices();

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

            builder.Services.AddMvc();

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
