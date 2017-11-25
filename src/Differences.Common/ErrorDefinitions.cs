using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Common
{
    public static class ErrorDefinitions
    {
        public static class User
        {
            public const string UserNotFound = "UserNotFound";
            public const string UserNotAuthenticated = "UserNotAuthenticated";

            public const string AccessDenied = "AccessDenied";
        }

        public static class Article
        {
            public const string ArticleNotExists = "ArticleNotExists";

            public const string CommentNotExists = "CommentNotExists";
        }

        public static class Question
        {
            public const string QuestionNotExists = "QuestionNotExists";

            public const string AnswerNotExists = "AnswerNotExists";
        }
    }
}
