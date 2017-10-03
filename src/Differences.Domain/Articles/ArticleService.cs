using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.Common;
using Differences.Domain.Users;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;

namespace Differences.Domain.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public ArticleService(
            IArticleRepository articleRepository,
            IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }

        public IReadOnlyList<Article> GetArticlesByCategory(long categoryId)
        {
            return _articleRepository.GetAll().ToList();
        }

        public Article WriteArticle(string title, string content, Guid userGuid)
        {
            var user = _userService.GetUserInfo(userGuid);
            if (user == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var article = new Article
            {
                AuthorId = user.Id,
                Title = title,
                Content = content
            };
            _articleRepository.Add(article);

            return article;
        }

        public Comment AddComment(long articleId, long? commentId, string content, Guid userGuid)
        {
            throw new NotImplementedException();
        }
    }
}
