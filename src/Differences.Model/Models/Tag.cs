using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Tag : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
