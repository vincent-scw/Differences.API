using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;

namespace GraphQL.Annotations
{
    public class AnnotableGraphType<TModel> : ObjectGraphType<TModel>
        where TModel : class
    {

    }
}
