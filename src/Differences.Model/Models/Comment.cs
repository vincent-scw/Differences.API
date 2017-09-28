using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Comment : AggregateRoot
    {
        [Required]
        public long ArticleId { get; set; }
        [Required]
        [StringLength(400)]
        public string Content { get; set; }
        [Required]
        public long OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
