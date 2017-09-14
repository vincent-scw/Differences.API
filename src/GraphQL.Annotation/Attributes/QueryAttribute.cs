using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQL.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class QueryAttribute : Attribute
    {
        public QueryAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
