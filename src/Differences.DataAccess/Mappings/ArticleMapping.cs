using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Differences.DataAccess.Mappings
{
    internal class ArticleMapping
    {
        public ArticleMapping(EntityTypeBuilder<Article> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(x => x.Content).IsRequired();

            entityTypeBuilder.HasOne(x => x.Author).WithMany(x => x.Articles).HasForeignKey(k => k.AuthorId);
        }
    }
}
