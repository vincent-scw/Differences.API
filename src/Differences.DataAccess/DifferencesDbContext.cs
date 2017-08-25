using Differences.Common.Configuration;
using Differences.Interaction.Models;
using Humanizer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Differences.DataAccess
{
    public class DifferencesDbContext
    {
        private readonly IMongoDatabase _database;

        public DifferencesDbContext(IOptions<DbConnectionSetting> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>(nameof(User).Pluralize());
        public IMongoCollection<Article> Articles => _database.GetCollection<Article>(nameof(Article).Pluralize());
        public IMongoCollection<Question> Questions => _database.GetCollection<Question>(nameof(Question).Pluralize());
        public IMongoCollection<Answer> Answers => _database.GetCollection<Answer>(nameof(Answer).Pluralize());

        public IMongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : Entity
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.Pluralize());
        }
    }
}
