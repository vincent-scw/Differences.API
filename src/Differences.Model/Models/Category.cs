using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.Models
{
    public class Category : Entity
    {
        [ExcludeFromCodeCoverage]
        private Category()
        {
        }

        public Category(string name, string description)
            : this()
        {
            Name = name;
            Description = description;
        }

        [Required]
        [StringLength(50)]
        public string Name { get; private set; }
        [StringLength(200)]
        public string Description { get; private set; }
    }
}
