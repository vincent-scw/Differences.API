using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLoader;
using GraphQL.Types;

namespace Differences.Api
{
    public static class ResolveFieldContextExtensions
    {
        /// <summary>
        /// Id is as string type
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="context"></param>
        /// <param name="fetchDelegate"></param>
        /// <returns></returns>
        public static IDataLoader<long, TReturn> GetDataLoader<TSource, TReturn>(this ResolveFieldContext<TSource> context,
            Func<IEnumerable<long>, Task<ILookup<long, TReturn>>> fetchDelegate)
        {
            //return ((GraphQLUserContext)context.UserContext).LoadContext.GetOrCreateLoader(context.FieldDefinition, fetchDelegate);
            return null;
        }
    }
}
