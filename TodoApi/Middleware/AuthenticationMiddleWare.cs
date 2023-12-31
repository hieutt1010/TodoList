namespace TodoApi.Middleware
{
    public class AuthenticationMiddleWare
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Custom logic to be executed before the next middleware
            // …
            await _next(context);
            // Custom logic to be executed after the next middleware
        }
    }
}