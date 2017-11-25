using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.DataTransferModels
{
    public class CriteriaModel
    {
        public int CategoryId { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}
