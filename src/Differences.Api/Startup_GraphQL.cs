﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
using Differences.Api.Queries;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Differences.Api
{
    public partial class Startup
    {
        private static void InjectGraphQL(IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddTransient<UserType>();
            services.AddTransient<ReplyType>();
            services.AddSingleton<DifferencesQuery>();
            services.AddSingleton<ISchema>(s => new GraphQLSchema(type => (GraphType) s.GetService(type)));
        }
    }
}
