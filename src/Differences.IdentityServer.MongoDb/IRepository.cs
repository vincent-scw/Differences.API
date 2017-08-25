using Differences.IdentityServer.MongoDb.Models;

namespace Differences.IdentityServer.MongoDb
{
    public interface IRepository
    {
        MongoDbUser GetUserByUsername(string username);
        MongoDbUser GetUserById(string id);
        bool ValidatePassword(string username, string plainTextPassword);
        MongoDbClient GetClient(string clientId);

    }
}
