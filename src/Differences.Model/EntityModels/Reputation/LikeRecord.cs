using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class LikeRecord : AggregateRoot
    {
        [ExcludeFromCodeCoverage]
        public LikeRecord()
        {
        }

        public LikeRecord(Guid userId, int subjectId)
        {
            UserId = userId;
            SubjectId = subjectId;
        }

        [Required]
        public Guid UserId { get; private set; }
        [ForeignKey("UserId")]
        public User User { get; private set; }
        [Required]
        public int SubjectId { get; private set; }
    }
}
