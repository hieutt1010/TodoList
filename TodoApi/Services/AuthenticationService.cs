using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApi.Configurations;
using TodoApi.Core.Models.User;
using TodoApi.Interfaces;

namespace TodoApi.Services
{
    public class AuthenticationService : IAuthentication<GoogleUser>
    {
        private readonly IMongoCollection<GoogleUser> _googleUserCollection;
        public AuthenticationService(IOptions<MongoDbSettings> todoListDatabaseSettings)
        {
            // Initialize my MongoDb client
            var mongoClient = new MongoClient(
                       todoListDatabaseSettings.Value.ConnectionString);

            // Connect to the MongoDb database
            var mongoDatabase = mongoClient.GetDatabase(
                todoListDatabaseSettings.Value.DatabaseName);

            _googleUserCollection = mongoDatabase
                .GetCollection<GoogleUser>(
                    todoListDatabaseSettings.Value.CollectionName);
        }

        public async Task<GoogleUser> CreateUser(GoogleUser user)
        {
            await _googleUserCollection.InsertOneAsync(user);
            return user;
        }

        public GoogleUser GetUserFromAuthenticateResult(AuthenticateResult authResult)
        {
            var claims = authResult.Principal.Identities.FirstOrDefault().Claims
            .Select(claim => new
            {
                claim.Type,
                claim.Value,
            });
            if (claims is not null)
            {
                var googleUser = new GoogleUser();
                foreach (var claim in claims)
                {
                    if (claim.Type == ClaimTypes.NameIdentifier)
                        googleUser.NameIdentifier = claim.Value;
                    // if (claim.Type == ClaimTypes.GivenName)
                    //     googleUser.UserName = claim.Value;
                }
                return googleUser;
            }
            return null;
        }
    }
}