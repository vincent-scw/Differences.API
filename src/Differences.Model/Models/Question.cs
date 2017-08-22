using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Question : AggregateRoot
    {
        public string Title { get; set; }
        public IEnumerable<KeyValueHolder> Tags { get; set; }
        public string Content { get; set; }
        public KeyValueHolder Owner { get; set; }
    }
}
