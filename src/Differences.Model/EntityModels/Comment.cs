using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Differences.Interaction.EntityModels
{
    public class Comment : Entity
    {
        [ExcludeFromCodeCoverage]
        public Comment()
        {
            this.SubComments = new List<Comment>();
        }

        public Comment(int articleId, string content, Guid ownerId)
            : this()
        {
            ArticleId = articleId;
            Content = content;
            OwnerId = ownerId;
        }

        public Comment(int articleId, int? parentCommentId, string content, Guid ownerId)
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
        public Guid OwnerId { get; private set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; private set; }
        [NotMapped]
        public List<Comment> SubComments { get; set; }

        public void Update(string content)
        {
            Content = content;
            LastUpdateTime = DateTime.Now;
        }
    }
}
