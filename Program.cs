using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc;
using ExamenOpdracht.Data;
using Microsoft.EntityFrameworkCore;

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

            // Voeg Mvc services toe in een aparte methode
            ConfigureMvcServices(builder.Services);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ExamenOpdrachtContext>();
                ExamenOpdrachtContext.ExamenOpdrachtContextSeed.SeedData(dbContext);
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
    }
}
