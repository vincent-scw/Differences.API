using System;
using System.Collections.Generic;
using System.Linq;
using Differences.Common;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;

namespace Differences.Domain.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly CategoryDefinition _categoryDefinition;

        public ArticleService(
            IArticleRepository articleRepository,
            IUserRepository userRepository,
            CategoryDefinition categoryDefinition)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _categoryDefinition = categoryDefinition;
        }

        public IReadOnlyList<Article> GetArticlesByCategory(int categoryId)
        {
            var categoryGroup = _categoryDefinition.GetCategoryGroup(categoryId);
            if (categoryGroup == null)
                return _articleRepository.GetAll().Where(x => x.CategoryId == categoryId)
                    .OrderByDescending(x => x.CreateTime).ToList();
            else
            {
                var ids = categoryGroup.Categories.Select(x => x.Id);
                return _articleRepository.GetAll().Where(x => ids.Contains(x.CategoryId)).OrderByDescending(x => x.CreateTime).ToList();
            }
        }

        public Article WriteArticle(SubjectModel subject, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var article = new Article(subject, userGuid);
            _articleRepository.Add(article);

            _articleRepository.SaveChanges();

            return article;
        }

        public Article UpdateArticle(SubjectModel subject, Guid userGuid)
        {
            var article = _articleRepository.Get(subject.Id);
            if (article == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.Article.ArticleNotExists };

            if (article.AuthorId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            article.Update(subject);
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
