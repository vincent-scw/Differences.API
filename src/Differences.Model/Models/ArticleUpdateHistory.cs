using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.Models
{
    public class ArticleUpdateHistory : TraceableEntity
    {
        [ExcludeFromCodeCoverage]
        public ArticleUpdateHistory()
        {
        }

        public ArticleUpdateHistory(int articleId, 
            string content,
            DataStatus status,
            Guid? userId)
            : this()
        {
            ArticleId = articleId;
            Content = content;
            Status = status;
            LastUpdatedBy = userId;
            LastUpdateTime = DateTime.Now;
        }

        [Required]
        public int ArticleId { get; private set; }
        [Required]
        public string Content { get; private set; }
    }
}
