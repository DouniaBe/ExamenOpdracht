using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ExamenOpdracht.Data;
using ExamenOpdracht.Models;
using Microsoft.EntityFrameworkCore;
using ExamenOpdracht.Middleware;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;

namespace ExamenOpdracht
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuratie voor Entity Framework Core
            builder.Services.AddDbContext<ExamenOpdrachtContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ExamenOpdrachtContext") ?? throw new InvalidOperationException("Connection string 'ExamenOpdrachtContext' not found.")));

            // Configuratie voor seeding van gegevens
            builder.Services.TryAddScoped<ExamenOpdrachtContext.ExamenOpdrachtContextSeed>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ExamenOpdrachtContext>()
                .AddDefaultTokenProviders();

            // Voeg Mvc services toe in een aparte methode
            ConfigureMvcServices(builder.Services);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ExamenOpdrachtContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                ExamenOpdrachtContext.ExamenOpdrachtContextSeed.SeedData(dbContext, userManager, roleManager);
            }

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        // Methode om Mvc services te configureren
        private static void ConfigureMvcServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddRazorPages();
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app)
        {
            // ...

            // Voeg jouw custom middleware toe
            app.UseCustomMiddleware();

            // ...
        }
        // ...

        public void ConfigureServices(IServiceCollection services)
        {
            // ...

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            // ...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ...

            var supportedCultures = new[]
            {
        new CultureInfo("en"),
        new CultureInfo("nl"),
        // Voeg meer talen toe indien nodig
    };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            // ...

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // ...
        }

    }
}
