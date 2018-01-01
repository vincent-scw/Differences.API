using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels.Contribution
{
    public class ContributeType
    {
        [ExcludeFromCodeCoverage]
        public ContributeType()
        {
            
        }

        public ContributeType(int id, string desc)
            : this()
        {
            Id = id;
            Description = desc;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
    }
}
