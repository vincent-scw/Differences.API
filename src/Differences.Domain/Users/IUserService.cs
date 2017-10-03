using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.Models;

namespace Differences.Domain.Users
{
    public interface IUserService
    {
        User GetUserInfo(Guid globalId);
        User FindOrCreate(Guid globalId, string displayName, string email, string avatarUrl);
        IReadOnlyList<User> GetTopReputationUsers(long categoryId, int topCount = 20);
    }
}
