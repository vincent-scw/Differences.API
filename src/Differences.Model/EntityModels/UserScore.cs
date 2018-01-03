using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class UserScore : AuditModel
    {
        [ExcludeFromCodeCoverage]
        public UserScore()
        {
            ContributionLogs = new List<UserContributionLog>();
            ReputationLogs = new List<UserReputationLog>();
        }

        public UserScore(Guid userId)
        {
            Id = userId;
        }

        [Key]
        public Guid Id { get; private set; }
        [ForeignKey("Id")]
        public User User { get; private set; }
        [Required]
        public int ContributeValue { get; private set; }
        [ForeignKey("UserId")]
        public virtual ICollection<UserContributionLog> ContributionLogs { get; private set; }
        [Required]
        public double ReputationValue { get; private set; }
        [ForeignKey("UserId")]
        public virtual ICollection<UserReputationLog> ReputationLogs { get; private set; }

        public void IncreaseContribution(int type, int value, int? subjectiId)
        {
            ContributeValue += value;
            LastUpdateTime = DateTime.Now;
            LastUpdatedBy = Id;
            this.ContributionLogs.Add(
                new UserContributionLog(Id, type, value, subjectiId) {CreateTime = DateTime.Now, CreatedBy = Id});
        }

        public void IncreaseReputation(int type, double value, int? subjectId)
        {
            ReputationValue += value;
            LastUpdateTime = DateTime.Now;
            LastUpdatedBy = Id;
            this.ReputationLogs.Add(
                new UserReputationLog(Id, type, value, subjectId) {CreateTime = DateTime.Now, CreatedBy = Id});
        }
    }
}