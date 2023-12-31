using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using TodoApi.Core.Models.GoogleAPI;

namespace TodoApi.Configurations
{
    public static class GoogleConfig
    {
        public static void AddGoogleAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Google Setting
            var servicesAPIKey = configuration["TodoList:ServiceApiKey"];
            var googleAPIConfig = configuration.GetSection("Authentication:Google").Get<GoogleSettings>();
            #region Google Auth
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = googleAPIConfig.ClientId;
                    googleOptions.ClientSecret = googleAPIConfig.ClientSecret;
                });
            #endregion
        }
    }
}