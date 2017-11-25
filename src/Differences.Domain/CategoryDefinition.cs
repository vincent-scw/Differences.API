using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.Interaction.EntityModels;

namespace Differences.Domain
{
    public static class CategoryDefinition
    {
        private static IList<CategoryGroup> categoryGroups = new List<CategoryGroup>
        {
            new CategoryGroup(1, "技术", "")
            {
                Categories = new List<Category>
                {
                    new Category(101, "IT", ""),
                    new Category(102, "机械", ""),
                    new Category(103, "电子", ""),
                    new Category(199, "其他", ""),
                }
            },
            new CategoryGroup(2, "科学与自然", "")
            {
                Categories = new List<Category>
                {
                    new Category(201, "科学", ""),
                    new Category(202, "自然", ""),
                }
            },
            new CategoryGroup(3, "语言", "")
            {
                Categories = new List<Category>
                {
                    new Category(301, "英语", ""),
                    new Category(399, "其他", ""),
                }
            },
            new CategoryGroup(4, "社科", "")
            {
                Categories = new List<Category>
                {
                    new Category(401, "历史", ""),
                    new Category(499, "其他", ""),
                }
            },
            new CategoryGroup(5, "人文", "")
            {
                Categories = new List<Category>
                {
                    new Category(501, "名人", ""),
                    new Category(599, "其他", ""),
                }
            },
            new CategoryGroup(9, "其他", "")
            {
                Categories = new List<Category>
                {
                    new Category(999, "其他", ""),
                }
            }
        };
        public static IList<CategoryGroup> CategoryGroups => categoryGroups;

        public static bool IsCategoryGroup(int id)
        {
            return categoryGroups.Any(x => x.Id == id);
        }

        public static CategoryGroup GetCategoryGroup(int id)
        {
            return categoryGroups.FirstOrDefault(x => x.Id == id);
        }
    }
}
