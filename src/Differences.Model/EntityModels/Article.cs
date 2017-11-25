using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Differences.Interaction.DataTransferModels;

namespace Differences.Interaction.EntityModels
{
    public class Article : AggregateRoot
    {
        [ExcludeFromCodeCoverage]
        public Article()
        {
            UpdateHistories = new List<ArticleUpdateHistory>();
            Comments = new List<Comment>();
        }

        public Article(string title,
            string content,
            int categoryId,
            string tags,
            Guid authorId)
            : this()
        {
            Title = title;
            Content = content;
            CategoryId = categoryId;
            Tags = tags;

            AuthorId = authorId;
        }

        [Required]
        [StringLength(100)]
        public string Title { get; private set; }
        [Required]
        public string Content { get; private set; }
        [Required]
        public int CategoryId { get; private set; }
        public string Tags { get; private set; }
        [Required]
        public Guid AuthorId { get; private set; }
        [ForeignKey("AuthorId")]
        public User Author { get; private set; }
        [ForeignKey("ArticleId")]
        public IList<ArticleUpdateHistory> UpdateHistories { get; private set; }
        [ForeignKey("ArticleId")]
        public IList<Comment> Comments { get; private set; }

        public void Update(string title,
            string content,
            int categoryId,
            string tags)
        {
            Title = title;
            Content = content;
            CategoryId = categoryId;
            Tags = tags;

            LastUpdateTime = DateTime.Now;
        }

        public void AddComment(Comment comment)
        {
            comment.CreateTime = DateTime.Now;
            comment.CreatedBy = comment.OwnerId;
            Comments.Add(comment);
        }
    }
}
