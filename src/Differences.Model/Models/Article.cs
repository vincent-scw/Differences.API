using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Article : AggregateRoot
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public KeyValueHolder Author { get; set; }
        public IEnumerable<KeyValueHolder> Contributors { get; set; }
    }
}
