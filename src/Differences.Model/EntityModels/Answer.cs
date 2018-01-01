using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class Answer : Entity
    {
        [ExcludeFromCodeCoverage]
        public Answer()
        {
            this.SubAnswers = new List<Answer>();
        }

        public Answer(int questionId, string content, Guid ownerId)
            : this()
        {
            QuestionId = questionId;
            Content = content;
            OwnerId = ownerId;
        }

        public Answer(int questionId, int? parentReplyId, string content, Guid ownerId)
            : this(questionId, content, ownerId)
        {
            ParentReplyId = parentReplyId;
        }

        [Required]
        public int QuestionId { get; private set; }
        public int? ParentReplyId { get; private set; }
        [Required]
        [Column("Content", TypeName = "ntext")]
        public string Content { get; private set; }
        [Required]
        public Guid OwnerId { get; private set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; private set; }
        [ForeignKey("ParentReplyId")]
        public List<Answer> SubAnswers { get; set; }

        public void Update(string content)
        {
            Content = content;
            LastUpdateTime = DateTime.Now;
            LastUpdatedBy = OwnerId;
        }
    }
}
