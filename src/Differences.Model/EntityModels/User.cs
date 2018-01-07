using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public class User : AuditModel
    {
        [ExcludeFromCodeCoverage]
        public User() { }

        public User(Guid globalId, string displayName)
            : this()
        {
            Id = globalId;
            DisplayName = displayName;
        }

        [Key]
        [ConcurrencyCheck]
        public Guid Id { get; protected set; }
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string DisplayName { get; private set; }
        [StringLength(200)]
        public string AvatarUrl { get; set; }
        [StringLength(50)]
        public string LinkedInId { get; set; }
        [StringLength(50)]
        public string MicrosoftId { get; set; }
        public virtual UserScore UserScores { get; private set; }

        public void Update(string displayName, string email)
        {
            DisplayName = displayName;
            Email = email;
            //AvatarUrl = url;
            LastUpdateTime = DateTime.Now;
            LastUpdatedBy = Id;
        }
    }
}
