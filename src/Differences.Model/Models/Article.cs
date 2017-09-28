using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Article : AggregateRoot
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public long AuthorId { get; set; }
        public User Author { get; set; }
        public IEnumerable<User> Contributors { get; set; }
    }
}
