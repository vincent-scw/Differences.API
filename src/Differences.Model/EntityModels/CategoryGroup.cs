using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class CategoryGroup : Category
    {
        public CategoryGroup(int id, string name, string description)
            : base(id, name, description)
        {
            this.Categories = new List<Category>();
        }

        public IList<Category> Categories { get; set; }
    }
}
