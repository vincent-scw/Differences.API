using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class UserReputationLog : Entity
    {
        [ExcludeFromCodeCoverage]
        public UserReputationLog() { }

        public UserReputationLog(Guid userId, int reputationTypeId, double value, int? subjectId)
            : this()
        {
            UserId = userId;
            ReputationTypeId = reputationTypeId;
            Value = value;
            SubjectId = subjectId;
        }

        [Required]
        public Guid UserId { get; private set; }
        [ForeignKey("UserId")]
        public User User { get; private set; }
        [Required]
        public int ReputationTypeId { get; private set; }
        [Required]
        public double Value { get; private set; }
        public int? SubjectId { get; private set; }
    }
}
