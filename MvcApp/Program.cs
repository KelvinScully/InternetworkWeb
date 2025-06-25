using Common;
using Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddRepository();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
