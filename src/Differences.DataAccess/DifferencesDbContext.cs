using System.Collections.Generic;
using Differences.Interaction.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Differences.DataAccess
{
    public class DifferencesDbContext : DbContext
    {
        public DifferencesDbContext(DbContextOptions<DifferencesDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleUpdateHistory> ArticleUpdateHistories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
