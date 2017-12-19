using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class Question : AggregateRoot
    {
        [ExcludeFromCodeCoverage]
        public Question()
        {
            Answers = new List<Answer>();
        }

        public Question(string title, string content, int categoryId, Guid ownerId)
            : this()
        {
            Title = title;
            Content = content;
            this.CategoryId = categoryId;
            OwnerId = ownerId;
        }

        [Required]
        [StringLength(100)]
        public string Title { get; private set; }
        [Required]
        [MaxLength(400)]
        public string Content { get; private set; }
        [Required]
        public int CategoryId { get; private set; }
        [Required]
        public Guid OwnerId { get; private set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; private set; }
        [ForeignKey("QuestionId")]
        public virtual ICollection<Answer> Answers { get; private set; }

        public void Update(string title, string content, int categoryId)
        {
            Title = title;
            Content = content;
            this.CategoryId = categoryId;
            LastUpdateTime = DateTime.Now;
        }

        public void AddAnswer(Answer reply)
        {
            reply.CreateTime = DateTime.Now;
            reply.CreatedBy = reply.OwnerId;
            Answers.Add(reply);
        }
    }
}
