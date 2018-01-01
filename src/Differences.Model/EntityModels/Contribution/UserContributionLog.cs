using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class UserContributionLog : Entity
    {
        [ExcludeFromCodeCoverage]
        public UserContributionLog() { }

        public UserContributionLog(Guid userId, int contributeTypeId, int value, int? subjectId)
            : this()
        {
            UserId = userId;
            ContributeTypeId = contributeTypeId;
            Value = value;
            SubjectId = subjectId;
        }

        [Required]
        public Guid UserId { get; private set; }
        [ForeignKey("UserId")]
        public User User { get; private set; }
        [ForeignKey("UserId")]
        public UserScore UserScore { get; private set; }
        [Required]
        public int ContributeTypeId { get; private set; }
        [Required]
        public int Value { get; private set; }
        public int? SubjectId { get; private set; }
    }
}