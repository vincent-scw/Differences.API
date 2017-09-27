using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Comment : AggregateRoot
    {
        public string ArticleId { get; set; }
        public string Content { get; set; }
        public User Owner { get; set; }
    }
}
