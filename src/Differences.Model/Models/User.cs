using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Differences.Interaction.Models
{
    public class User : AggregateRoot
    {
        [ExcludeFromCodeCoverage]
        private User() { }

        public User(Guid globalId, string displayName, string email, string avatarUrl)
            : this()
        {
            GlobalId = globalId;
            DisplayName = displayName;
            Email = email;
            AvatarUrl = avatarUrl;
        }

        [Required]
        [ConcurrencyCheck]
        public Guid GlobalId { get; private set; }
        [Required]
        [StringLength(100)]
        public string Email { get; private set; }
        [Required]
        [StringLength(100)]
        public string DisplayName { get; private set; }
        [StringLength(200)]
        public string AvatarUrl { get; private set; }

        public void UpdateAvatar(string url)
        {
            AvatarUrl = url;
            LastUpdateTime = DateTime.Now;
        }
    }
}
