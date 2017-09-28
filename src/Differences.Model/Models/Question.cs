using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Question : AggregateRoot
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(400)]
        public string Content { get; set; }
        [Required]
        public long OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
        [ForeignKey("QuestionId")]
        public IEnumerable<Reply> Replies { get; set; }
    }
}
