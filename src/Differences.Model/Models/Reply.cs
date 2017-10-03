using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Reply : Entity
    {
        [ExcludeFromCodeCoverage]
        private Reply() { }

        public Reply(long questionId, string content, long ownerId)
            : this()
        {
            QuestionId = questionId;
            Content = content;
            OwnerId = ownerId;
        }

        public Reply(long questionId, long parentReplyId, string content, long ownerId)
            : this(questionId, content, ownerId)
        {
            ParentReplyId = parentReplyId;
        }

        [Required]
        public long QuestionId { get; private set; }
        public long? ParentReplyId { get; private set; }
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
