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

            public const string TitleLengthTooShort = "QuestionTitleLengthTooShort";
            public const string TitleLengthExceeding = "QuestionTitleLengthExceeding";
            public const string ContentLengthTooShort = "QuestionContentLengthTooShort";
            public const string ContentLengthExceeding = "QuestionContentLengthExceeding";
        }

        public static class Answer
        {
            public const string AnswerNotExists = "AnswerNotExists";

            public const string ContentLengthTooShort = "AnswerContentLengthTooShort";
            public const string ContentLengthExceeding = "AnswerContentLengthExceeding";
        }
    }
}
