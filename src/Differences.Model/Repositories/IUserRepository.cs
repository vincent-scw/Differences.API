using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Differences.Interaction.Repositories
{
    public interface IUserRepository
    {
        bool Exists(Guid userId);

        User Get(Guid userId);

        User Add(User user);

        IQueryable<User> GetAll();

        void SaveChanges();
    }
}
