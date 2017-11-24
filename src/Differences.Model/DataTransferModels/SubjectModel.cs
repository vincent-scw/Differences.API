using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.DataTransferModels
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string Tags { get; set; }
    }
}
