using System.ComponentModel.DataAnnotations;

namespace ExamenOpdracht.Models
{
    public class Gebruiker
    {
        public int GebruikerId { get; set; }

        [Required]
        public string GebruikerNaam { get; set; }

        // Andere eigenschappen van Gebruiker
    }
}
