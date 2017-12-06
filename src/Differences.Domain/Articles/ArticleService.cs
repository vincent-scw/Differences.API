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

        public ArticleService(
            IArticleRepository articleRepository,
            IUserRepository userRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
        }

        public IReadOnlyList<Article> GetArticlesByCategory(CriteriaModel criteria)
        {   
            return GetSearchCondition(criteria.CategoryId).Skip(criteria.Offset ?? 0).Take(criteria.Limit ?? 0).ToList();
        }

        public int GetArticleCountByCategory(CriteriaModel criteria)
        {
            return GetSearchCondition(criteria.CategoryId).Count();
        }

        private IQueryable<Article> GetSearchCondition(int categoryId)
        {
            var categoryGroup = CategoryDefinition.GetCategoryGroup(categoryId);
            if (categoryGroup == null)
                return _articleRepository.GetAll().Where(x => x.CategoryId == categoryId)
                    .OrderByDescending(x => x.CreateTime);
            else
            {
                var ids = categoryGroup.Categories.Select(x => x.Id);
                return _articleRepository.GetAll().Where(x => ids.Contains(x.CategoryId))
                    .OrderByDescending(x => x.CreateTime);
            }
        }

        public Article WriteArticle(SubjectModel subject, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var article = new Article(subject.Title, subject.Content, subject.CategoryId, subject.Tags, userGuid);
            _articleRepository.Add(article);

            _articleRepository.SaveChanges();

            _articleRepository.LoadReference(article, x => x.Author);
            return article;
        }

        public Article UpdateArticle(SubjectModel subject, Guid userGuid)
        {
            var article = _articleRepository.Get(subject.Id);
            if (article == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.Article.ArticleNotExists };

            if (article.AuthorId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            article.Update(subject.Title, subject.Content, subject.CategoryId, subject.Tags);
            _articleRepository.SaveChanges();

            _articleRepository.LoadReference(article, x => x.Author);
            return article;
        }

        public Comment AddComment(ReplyModel reply, Guid userGuid)
        {
            var article = _articleRepository.Get(reply.SubjectId);
            if (article == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.Article.ArticleNotExists };

            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var comment = new Comment(reply.SubjectId, reply.ParentId, reply.Content, userGuid);
            article.AddComment(comment);

            _articleRepository.SaveChanges();

            _articleRepository.LoadReference(comment, x => x.Owner);
            return comment;
        }

        public Comment UpdateComment(ReplyModel reply, Guid userGuid)
        {
            var comment = _articleRepository.GetComment(reply.Id);
            if (comment == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Article.CommentNotExists};

            if (comment.OwnerId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            comment.Update(reply.Content);
            _articleRepository.SaveChanges();

            _articleRepository.LoadReference(comment, x => x.Owner);
            return comment;
        }

        public IReadOnlyList<Comment> GetCommentsByArticleId(int articleId)
        {
            var comments = _articleRepository.GetComments(articleId);
            var firstLevel = comments.Where(x => x.ParentCommentId == null).ToList();
            var secondLevel = comments.Where(x => x.ParentCommentId != null).ToList();

            firstLevel.ForEach(f =>
            {
                f.SubComments.AddRange(secondLevel.Where(s => s.ParentCommentId == f.Id).OrderBy(x => x.CreateTime));
            });

            return firstLevel.OrderByDescending(x => x.CreateTime).ToList();
        }
    }
}
