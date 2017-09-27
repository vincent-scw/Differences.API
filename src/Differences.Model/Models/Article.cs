using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Article : AggregateRoot
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public long AuthorId { get; set; }
        public User Author { get; set; }
        public IEnumerable<User> Contributors { get; set; }
    }
}
