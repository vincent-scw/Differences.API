using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Differences.Interaction.Models
{
    public class User : AggregateRoot
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }
        [StringLength(200)]
        public string AvatarUrl { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}
