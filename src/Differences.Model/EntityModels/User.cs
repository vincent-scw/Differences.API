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

        public User(Guid globalId, string displayName, string email, string avatarUrl)
            : this()
        {
            Id = globalId;
            DisplayName = displayName;
            Email = email;
            AvatarUrl = avatarUrl;
        }

        [Key]
        [ConcurrencyCheck]
        public Guid Id { get; protected set; }
        [Required]
        [StringLength(100)]
        public string Email { get; private set; }
        [Required]
        [StringLength(100)]
        public string DisplayName { get; private set; }
        [StringLength(200)]
        public string AvatarUrl { get; private set; }

        public virtual UserScore UserScores { get; private set; }

        public void Update(string displayName)
        {
            DisplayName = displayName;
            //Email = email;
            //AvatarUrl = url;
            LastUpdateTime = DateTime.Now;
        }
    }
}
