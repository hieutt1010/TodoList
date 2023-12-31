using Microsoft.AspNetCore.Authentication;
namespace TodoApi.Interfaces
{
    public interface IAuthentication<T>
    {
        Task<T> CreateUser(T User);
        T GetUserFromAuthenticateResult(AuthenticateResult claim);
    }
}