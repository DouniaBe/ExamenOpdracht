using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExamenOpdracht.Models;

namespace ExamenOpdracht.Data
{
    public class ExamenOpdrachtContext : DbContext
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
            public static void SeedData(ExamenOpdrachtContext context)
            {
                SeedProducts(context);
                SeedUsers(context);
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

            private static void SeedUsers(ExamenOpdrachtContext context)
            {
                if (!context.Gebruiker.Any())
                {
                    var users = new List<Gebruiker>
                    {
                        new Gebruiker { GebruikerNaam = "JohnDoe" },
                        new Gebruiker { GebruikerNaam = "JaneDoe" },
                        // Voeg meer gebruikers toe indien nodig
                    };

                    context.Gebruiker.AddRange(users);
                    context.SaveChanges();
                }
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
