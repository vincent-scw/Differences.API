using System.Collections.Generic;
using Differences.Common.Configuration;
using Differences.Interaction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Differences.DataAccess
{
    public class DifferencesDbContext : DbContext
    {
        private readonly IOptions<DbConnectionSettings> _settings;

        public DifferencesDbContext(IOptions<DbConnectionSettings> settings)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.Value.Differences);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reply> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
