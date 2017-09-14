using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQL.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class QueriesDefinitionAttribute : Attribute
    {
    }
}
