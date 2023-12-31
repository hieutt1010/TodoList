using Microsoft.AspNetCore.Authentication;

namespace TodoApi.Configurations
{
    public static class MiddlewareConfig
    {
        public static void AddCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}