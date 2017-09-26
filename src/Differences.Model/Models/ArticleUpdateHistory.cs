using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Models
{
    public class ArticleUpdateHistory : TraceableEntity
    {
        public long ArticleId { get; set; }
        public string Content { get; set; }
    }
}
