using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.Models;

namespace Differences.Domain.Users
{
    public interface IUserService
    {
        User GetUserInfo(Guid globalId);
        IEnumerable<User> GetTopReputationUsers(long categoryId, int topCount);
    }
}
