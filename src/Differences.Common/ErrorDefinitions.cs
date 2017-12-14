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

        public static class Question
        {
            public const string QuestionNotExists = "QuestionNotExists";

            public const string AnswerNotExists = "AnswerNotExists";

            public const string TitleLengthExceeding = "TitleLengthExceeding";
            public const string ContentLengthExceeding = "ContentLengthExceeding";
        }
    }
}
