using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.DataTransferModels
{
    public class ReplyModel
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string Content { get; set; }
        public int? ParentId { get; set; }
    }
}
