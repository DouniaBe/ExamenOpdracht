using System.ComponentModel.DataAnnotations;

namespace ExamenOpdracht.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public decimal Prijs { get; set; }

        public string Beschrijving { get; set; }

        // Andere eigenschappen van Product

        public virtual ICollection<Bestelling> Bestellingen { get; set; }
        public DateTime Ended { get; internal set; }
    }
}
