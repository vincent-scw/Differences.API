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
        private Question()
        {
            Replies = new List<Reply>();
        }

        public Question(string title, string content, int ownerId)
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
        public int OwnerId { get; private set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; private set; }
        [ForeignKey("QuestionId")]
        public IList<Reply> Replies { get; private set; }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
            LastUpdateTime = DateTime.Now;
        }

        public void AddReply(Reply reply)
        {
            Replies.Add(reply);
        }
    }
}
