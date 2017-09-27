using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Question : AggregateRoot
    {
        public string Title { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public string Content { get; set; }
        public User Owner { get; set; }
    }
}
