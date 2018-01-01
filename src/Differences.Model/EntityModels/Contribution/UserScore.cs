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
            ContributionLog = new List<UserContributionLog>();
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
        public virtual ICollection<UserContributionLog> ContributionLog { get; private set; }

        public void IncreaseContribution(int type, int value, int? subjectiId)
        {
            ContributeValue += value;
            this.ContributionLog.Add(new UserContributionLog(Id, type, value, subjectiId));
        }

        public void DecreaseContribution(int type, int value, int? subjectId)
        {
            ContributeValue -= value;
            this.ContributionLog.Add(new UserContributionLog(Id, type, value, subjectId));
        }
    }
}