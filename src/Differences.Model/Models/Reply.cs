using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Reply : AggregateRoot
    {
        public string QuestionId { get; set; }
        public string Content { get; set; }
        public KeyValueHolder Owner { get; set; }
    }
}
