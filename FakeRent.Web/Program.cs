using FakeRent.Web.Services;
using FakeRent.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FakeRent.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Mapping
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            //Context
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                    options.SlidingExpiration = true;
                });

            //House Service DI
            builder.Services.AddHttpClient<IHouseService, HouseService>();
            builder.Services.AddScoped<IHouseService, HouseService>();
            //HouseNumber Service DI
            builder.Services.AddHttpClient<IHouseNumberService, HouseNumberService>();
            builder.Services.AddScoped<IHouseNumberService, HouseNumberService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            //Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}