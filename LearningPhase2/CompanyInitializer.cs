namespace LearningPhase2
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CompanyInitializer
    {
        private readonly RequestDelegate _next;

        public CompanyInitializer(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string url = "https://HRS.localhost:44357";
            string? subdomain = null;
            var uri = new Uri(url);
            var host = uri.Host;
            var segments = host.Split('.');
            if (segments.Length >= 1)
            {
                subdomain = segments[0];
            }

            if (!string.IsNullOrEmpty(subdomain))
            {

                context.Session.SetString("Subdomain", subdomain);
                context.Items["Subdomain"] = subdomain;

            }

            // Check if session is expired or missing
            var sessionSubdomain = context.Session.GetString("Subdomain");
            if (string.IsNullOrEmpty(sessionSubdomain))
            {
                // Handle session expiration (e.g., redirect to login)
                context.Response.Redirect("/Login"); // adjust path as needed
                return;
            }


            await _next(context);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CompanyInitializerExtensions
    {
        public static IApplicationBuilder UseCompanyInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CompanyInitializer>();
        }
    }
}
