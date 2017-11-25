using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class Category
    {
        [ExcludeFromCodeCoverage]
        public Category()
        {
        }

        public Category(int id, string name, string description)
            : this()
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
