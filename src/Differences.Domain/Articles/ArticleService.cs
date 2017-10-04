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

        public IReadOnlyList<Article> GetArticlesByCategory(int categoryId)
        {
            return _articleRepository.GetAll().ToList();
        }

        public Article WriteArticle(string title, string content, Guid userGuid)
        {
            var user = _userService.GetUserInfo(userGuid);
            if (user == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var article = new Article(title, content, user.Id);
            _articleRepository.Add(article);

            _articleRepository.SaveChanges();

            return article;
        }

        public Comment AddComment(int articleId, int? parentCommentId, string content, Guid userGuid)
        {
            var article = _articleRepository.Get(articleId);
            if (article == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.Article.ArticleNotExists };

            var user = _userService.GetUserInfo(userGuid);
            if (user == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var comment = new Comment(articleId, parentCommentId, content, user.Id);
            article.AddComment(comment);

            _articleRepository.SaveChanges();

            return comment;
        }
    }
}
