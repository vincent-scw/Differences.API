using System.Collections.Generic;
using Differences.Common.Configuration;
using Differences.DataAccess.Mappings;
using Differences.Interaction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Differences.DataAccess
{
    public class DifferencesDbContext : DbContext
    {
        public DifferencesDbContext(DbContextOptions<DifferencesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ArticleMapping(modelBuilder.Entity<Article>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reply> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
