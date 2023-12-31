namespace TodoApi.Configurations
{
    public static class CorsConfig
    {
        public static void AddCustomeCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                    policy.WithExposedHeaders("Access-Control-Allow-Origin");
                    policy.WithOrigins("http://localhost:4200", "https://localhost:7219");
                });
            });
        }
    }
}