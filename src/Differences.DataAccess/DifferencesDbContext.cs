using Differences.Interaction.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IMongoCollection<User> Users => _database.GetCollection<User>("User");

        public IMongoCollection<Article> Articles => _database.GetCollection<Article>("Article");

        public IMongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : Entity
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}
