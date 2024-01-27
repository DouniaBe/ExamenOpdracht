using Microsoft.AspNetCore.Identity;

namespace ExamenOpdracht.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        // Voeg andere eigenschappen toe die je nodig hebt
    }

}
