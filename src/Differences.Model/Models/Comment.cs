using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Differences.Interaction.Models
{
    public class Comment : Entity
    {
        [ExcludeFromCodeCoverage]
        public Comment() { }

        public Comment(int articleId, string content, int ownerId)
            : this()
        {
            ArticleId = articleId;
            Content = content;
            OwnerId = ownerId;
        }

        public Comment(int articleId, int? parentCommentId, string content, int ownerId)
            : this(articleId, content, ownerId)
        {
            ParentCommentId = parentCommentId;
        }

        [Required]
        public int ArticleId { get; private set; }
        public int? ParentCommentId { get; private set; }
        [Required]
        [StringLength(400)]
        public string Content { get; private set; }
        [Required]
        public int OwnerId { get; private set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; private set; }

        public void Update(string content)
        {
            Content = content;
            LastUpdateTime = DateTime.Now;
        }
    }
}
