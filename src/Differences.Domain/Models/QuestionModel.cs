using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Models
{
    public class QuestionModel
    {
        private readonly Question _question;
        public QuestionModel(Question question)
        {
            _question = question;
        }

        public int Id => _question.Id;
        public string Title => _question.Title;
        public string Content => _question.Content;
        public int CategoryId => _question.CategoryId;
        public string CategoryName => CategoryDefinition.GetCategoryString(_question.CategoryId);
        public DateTime CreateTime => _question.CreateTime;
        public DateTime? LastUpdateTime => _question.LastUpdateTime;
        public User User => _question.Owner;

        public int AnswerCount { get; set; }
    }
}
