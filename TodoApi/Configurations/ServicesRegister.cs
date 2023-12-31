using TodoApi.Core.Models.User;
using TodoApi.Interfaces;
using TodoApi.Services;

namespace TodoApi.Configurations
{
    public static class ServicesRegister
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Register services
            services.AddSingleton<ITodoList, TodoListService>();
            services.AddSingleton<IAuthentication<GoogleUser>, AuthenticationService>();
        }
    }
}