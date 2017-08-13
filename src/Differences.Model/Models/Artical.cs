using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Artical : AggregateRoot
    {
        public string Title { get; set; }
        public object Content { get; set; }
    }
}
