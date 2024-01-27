// YourEmailSenderImplementation.cs

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ExamenOpdracht.Services
{
    public class YourEmailSenderImplementation : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Implementeer de logica om een e-mail te verzenden
            // Dit kan gebruikmaken van een externe service zoals SendGrid, SMTP, etc.
            // Voor dit voorbeeld sturen we het alleen naar de uitvoer (console).
            Console.WriteLine($"Sending email to: {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {htmlMessage}");

            return Task.CompletedTask;
        }
    }
}
