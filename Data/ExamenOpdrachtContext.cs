using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExamenOpdracht.Models;
using Microsoft.AspNetCore.Identity;

namespace ExamenOpdracht.Data
{
    public class ExamenOpdrachtContext : IdentityDbContext<ApplicationUser>
    {
        public ExamenOpdrachtContext(DbContextOptions<ExamenOpdrachtContext> options)
            : base(options)
        {
        }

        public DbSet<ExamenOpdracht.Models.Bestelling> Bestelling { get; set; } = default!;
        public DbSet<ExamenOpdracht.Models.Gebruiker> Gebruiker { get; set; } = default!;
        public DbSet<ExamenOpdracht.Models.Product> Product { get; set; } = default!;

        public class ExamenOpdrachtContextSeed
        {
            public static void SeedData(ExamenOpdrachtContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                SeedProducts(context);
                SeedUsers(context, userManager);
                SeedRoles(roleManager);
                SeedUserRoles(context, userManager);
                SeedOrders(context);
            }

            private static void SeedProducts(ExamenOpdrachtContext context)
            {
                if (!context.Product.Any())
                {
                    var products = new List<Product>
                    {
                        new Product { Naam = "CV Maker", Prijs = 29.99M, Beschrijving = "Professionele CV Maker tool."},
                        new Product { Naam = "Logo Ontwerper", Prijs = 49.99M, Beschrijving = "Krachtige tool voor logo-ontwerp."},
                        new Product { Naam = "Flyers", Prijs = 14.99M, Beschrijving = "Hoogwaardige flyers voor evenementen."},
                        new Product { Naam = "Gepersonaliseerde Kaders", Prijs = 39.99M, Beschrijving = "Maatwerk kaders voor speciale momenten."},
                        // Voeg meer producten toe indien nodig
                    };

                    context.Product.AddRange(products);
                    context.SaveChanges();
                }
            }

            private static void SeedUsers(ExamenOpdrachtContext context, UserManager<ApplicationUser> userManager)
            {
                if (!context.Gebruiker.Any())
                {
                    var users = new List<ApplicationUser>
                    {
                        new ApplicationUser { UserName = "JohnDoe", Voornaam = "John", Achternaam = "Doe", Email = "john@example.com" },
                        new ApplicationUser { UserName = "JaneDoe", Voornaam = "Jane", Achternaam = "Doe", Email = "jane@example.com" },
                        // Voeg meer gebruikers toe indien nodig
                    };

                    foreach (var user in users)
                    {
                        userManager.CreateAsync(user, "Wachtwoord123!").Wait();
                    }
                }
            }

            private static void SeedRoles(RoleManager<IdentityRole> roleManager)
            {
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = new IdentityRole { Name = "Admin" };
                    roleManager.CreateAsync(role).Wait();
                }

                if (!roleManager.RoleExistsAsync("User").Result)
                {
                    var role = new IdentityRole { Name = "User" };
                    roleManager.CreateAsync(role).Wait();
                }
            }

            private static void SeedUserRoles(ExamenOpdrachtContext context, UserManager<ApplicationUser> userManager)
            {
                var user = userManager.FindByNameAsync("JohnDoe").Result;
                userManager.AddToRoleAsync(user, "Admin").Wait();

                user = userManager.FindByNameAsync("JaneDoe").Result;
                userManager.AddToRoleAsync(user, "User").Wait();
            }

            private static void SeedOrders(ExamenOpdrachtContext context)
            {
                if (!context.Bestelling.Any())
                {
                    var orders = new List<Bestelling>
                    {
                        new Bestelling { BestellingNaam = "Bestelling 1", Datum = DateTime.Now, Aantal = 2, ProductId = 1, GebruikerId = 1 },
                        new Bestelling { BestellingNaam = "Bestelling 2", Datum = DateTime.Now, Aantal = 1, ProductId = 3, GebruikerId = 2 },
                        // Voeg meer bestellingen toe indien nodig
                    };

                    context.Bestelling.AddRange(orders);
                    context.SaveChanges();
                }
            }
        }
    }
}
