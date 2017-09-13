using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLoader;

namespace Differences.Api
{
    public class GraphQLUserContext
    {
        public DataLoaderContext LoadContext { get; }

        public GraphQLUserContext(DataLoaderContext loadContext)
        {
            LoadContext = loadContext;
        }
    }
}
