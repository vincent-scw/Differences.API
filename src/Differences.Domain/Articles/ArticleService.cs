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
        private readonly IUserRepository _userRepository;

        public ArticleService(
            IArticleRepository articleRepository,
            IUserRepository userRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
        }

        public IReadOnlyList<Article> GetArticlesByCategory(int categoryId)
        {
            return _articleRepository.GetAll().ToList();
        }

        public Article WriteArticle(int categoryId, string title, string content, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var article = new Article(title, content, userGuid);
            _articleRepository.Add(article);

            _articleRepository.SaveChanges();

            return article;
        }

        public Article UpdateArticle(int articleId, int categoryId, string title, string content, Guid userGuid)
        {
            var article = _articleRepository.Get(articleId);
            if (article == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.Article.ArticleNotExists };

            if (article.AuthorId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            article.Update(title, content);
            _articleRepository.SaveChanges();
            return article;
        }

        public Comment AddComment(int articleId, int? parentCommentId, string content, Guid userGuid)
        {
            var article = _articleRepository.Get(articleId);
            if (article == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.Article.ArticleNotExists };

            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var comment = new Comment(articleId, parentCommentId, content, userGuid);
            article.AddComment(comment);

            _articleRepository.SaveChanges();

            return comment;
        }

        public Comment UpdateComment(int commentId, string content, Guid userGuid)
        {
            var comment = _articleRepository.GetComment(commentId);
            if (comment == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Article.CommentNotExists};

            if (comment.OwnerId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            comment.Update(content);
            _articleRepository.SaveChanges();
            return comment;
        }
    }
}
