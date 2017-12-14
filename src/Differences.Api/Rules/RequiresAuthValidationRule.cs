using System;
using System.Linq;
using Differences.Common;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Validation;

namespace Differences.Api.Rules
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

                // TODO: per field authentication check
                _.Match<Field>(fieldAst =>
                {
                    var fieldDef = context.TypeInfo.GetFieldDef();
                    if (fieldDef == null)
                    {
                        return;
                    }
                });
            });
        }
    }
}
