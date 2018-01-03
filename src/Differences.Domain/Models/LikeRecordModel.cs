using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.Models
{
    public class AnswerLikeModel
    {
        public int AnswerId { get; set; }
        public int LikeCount { get; set; }
        public bool Liked { get; set; }
    }
}
