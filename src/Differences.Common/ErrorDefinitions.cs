using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Common
{
    public static class ErrorDefinitions
    {
        public static class User
        {
            public const string UserNotFound = "U10001";
            public const string UserNotAuthenticated = "U10002";

            public const string AccessDenied = "U10009";
        }

        public static class Article
        {
            public const string ArticleNotExists = "A10001";

            public const string CommentNotExists = "A20001";
        }

        public static class Question
        {
            public const string QuestionNotExists = "Q10001";

            public const string AnswerNotExists = "Q20001";
        }
    }
}
