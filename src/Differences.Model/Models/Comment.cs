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
        private Comment() { }

        public Comment(long articleId, string content, long ownerId)
            : this()
        {
            ArticleId = articleId;
            Content = content;
            OwnerId = ownerId;
        }

        public Comment(long articleId, long parentCommentId, string content, long ownerId)
            : this(articleId, content, ownerId)
        {
            ParentCommentId = parentCommentId;
        }

        [Required]
        public long ArticleId { get; private set; }
        public long? ParentCommentId { get; private set; }
        [Required]
        [StringLength(400)]
        public string Content { get; private set; }
        [Required]
        public long OwnerId { get; private set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; private set; }

        public void Update(string content)
        {
            Content = content;
            LastUpdateTime = DateTime.Now;
        }
    }
}
