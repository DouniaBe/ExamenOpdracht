using System.ComponentModel.DataAnnotations;

namespace ExamenOpdracht.Models
{
    public class Bestelling
    {
        public int BestellingId { get; set; }

        [Required]
        public string BestellingNaam { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        [Required]
        public int Aantal { get; set; }

        // Relatie met Product
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Relatie met Gebruiker
        public int GebruikerId { get; set; }
        public virtual Gebruiker Gebruiker { get; set; }
    }
}
