using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Models
{
    public class UserWithTokenModel
    {
        public string AccessToken { get; set; }
        public User User { get; set; }
    }
}
