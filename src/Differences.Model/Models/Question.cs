using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Question : AggregateRoot
    {
        [ExcludeFromCodeCoverage]
        public Question()
        {
            //Answers = new List<Answer>();
        }

        public Question(string title, string content, Guid ownerId)
            : this()
        {
            Title = title;
            Content = content;
            OwnerId = ownerId;
        }

        [Required]
        [StringLength(100)]
        public string Title { get; private set; }
        [Required]
        [StringLength(400)]
        public string Content { get; private set; }
        [Required]
        public Guid OwnerId { get; private set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; private set; }
        [ForeignKey("QuestionId")]
        public virtual ICollection<Answer> Answers { get; private set; }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
            LastUpdateTime = DateTime.Now;
        }

        public void AddAnswer(Answer reply)
        {
            Answers.Add(reply);
        }
    }
}
