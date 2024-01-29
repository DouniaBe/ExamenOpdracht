using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace ExamenOpdracht.Middleware
{


    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Lees de waarde van de cookie met de naam "MijnCookie"
            var cookieValue = context.Request.Cookies["MijnCookie"];

            // Zet de waarde in context.Items als een globale variabele met de naam "GlobaleVariabele"
            context.Items["GlobaleVariabele"] = cookieValue;

            // Roep het volgende middleware in de pipeline aan
            await _next(context);
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }

}
