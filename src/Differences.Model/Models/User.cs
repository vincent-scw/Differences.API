using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class User : AggregateRoot
    {
        public string Name { get; set; }
        public string DisplaykName { get; set; }
        public string AvatarUrl { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}
