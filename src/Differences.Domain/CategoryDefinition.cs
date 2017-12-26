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
            new CategoryGroup(1, "科学与自然", "")
            {
                Categories = new List<Category>
                {
                    new Category(101, "科学", ""),
                    new Category(102, "自然", ""),
                }
            },
            new CategoryGroup(2, "技术", "")
            {
                Categories = new List<Category>
                {
                    new Category(201, "IT", ""),
                    new Category(202, "机械", ""),
                    new Category(203, "电子", ""),
                    new Category(299, "其他", ""),
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
            new CategoryGroup(4, "社科人文", "")
            {
                Categories = new List<Category>
                {
                    new Category(401, "历史", ""),
                    new Category(402, "文化", ""),
                    new Category(403, "名人", ""),
                    new Category(499, "其他", ""),
                }
            },
            new CategoryGroup(5, "游戏娱乐", "")
            {
                Categories = new List<Category>
                {
                    new Category(501, "游戏", ""),
                    new Category(502, "娱乐", "")
                }
            },
            new CategoryGroup(6, "体育运动", "")
            {
                Categories = new List<Category>
                {
                    new Category(601, "足球", ""),
                    new Category(602, "篮球", ""),
                    new Category(699, "其他", "")
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

        public static string GetCategoryString(int id)
        {
            if (id < 10)
                return categoryGroups.FirstOrDefault(x => x.Id == id)?.Name;

            var groupId = id / 100;
            var group = categoryGroups.FirstOrDefault(x => x.Id == groupId);
            return @group?.Categories.FirstOrDefault(x => x.Id == id)?.Name;
        }
    }
}
