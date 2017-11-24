using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Users
{
    public interface IUserService
    {
        User FindOrCreate(Guid globalId, string displayName, string email, string avatarUrl);
        IReadOnlyList<User> GetTopReputationUsers(int categoryId, int topCount = 20);
    }
}
