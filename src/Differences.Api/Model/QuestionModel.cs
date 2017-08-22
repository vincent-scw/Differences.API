using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Differences.Api.Model
{
    public class QuestionModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IList<string> Tags { get; set; }
        public string Content { get; set; }
    }
}
