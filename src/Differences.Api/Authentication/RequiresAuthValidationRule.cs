using System;
using Differences.Common;
using GraphQL.Language.AST;
using GraphQL.Validation;

namespace Differences.Api.Authentication
{
    public class RequiresAuthValidationRule : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            var userContext = context.UserContext as GraphQLUserContext;
            var authenticated = userContext.IsAuthenticated;

            return new EnterLeaveListener(_ =>
            {
                _.Match<Operation>(op =>
                {
                    if (op.OperationType == OperationType.Mutation && !authenticated)
                    {
                        context.ReportError(new ValidationError(
                            context.OriginalQuery, 
                            ErrorDefinitions.User.AccessDenied,
                            $"Authorization is required to access {op.Name}",
                            op));
                    }
                });

                // TODO: per pield authentication check
            });
        }
    }
}
