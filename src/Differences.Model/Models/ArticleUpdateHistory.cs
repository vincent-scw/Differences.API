using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Differences.Interaction.Models
{
    public class ArticleUpdateHistory : TraceableEntity
    {
        [Required]
        public long ArticleId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
