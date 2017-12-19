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

            public const string AccessDenied = "UserAccessDenied";
        }

        public static class Question
        {
            public const string QuestionNotExists = "QuestionNotExists";

            public const string AnswerNotExists = "QuestionAnswerNotExists";

            public const string TitleLengthExceeding = "QuestionTitleLengthExceeding";
            public const string ContentLengthExceeding = "QuestionContentLengthExceeding";
        }
    }
}
