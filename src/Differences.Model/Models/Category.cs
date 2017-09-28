using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Category : Entity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
    }
}
