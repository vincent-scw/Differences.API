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
        public Reply() { }

        public Reply(int questionId, string content, int ownerId)
            : this()
        {
            QuestionId = questionId;
            Content = content;
            OwnerId = ownerId;
        }

        public Reply(int questionId, int? parentReplyId, string content, int ownerId)
            : this(questionId, content, ownerId)
        {
            ParentReplyId = parentReplyId;
        }

        [Required]
        public int QuestionId { get; private set; }
        public int? ParentReplyId { get; private set; }
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
