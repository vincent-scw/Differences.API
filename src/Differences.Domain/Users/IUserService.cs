using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Users
{
    public interface IUserService
    {
        User FindOrCreate();
        User UpdateUser(UserModel user);
        IReadOnlyList<User> GetTopReputationUsers(int categoryId, int topCount = 20);
    }
}
