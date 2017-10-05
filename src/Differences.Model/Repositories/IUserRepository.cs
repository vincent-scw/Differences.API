using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Differences.Interaction.Repositories
{
    public interface IUserRepository
    {
        User Get(Guid globalId);

        User Add(User user);

        IQueryable<User> GetAll();

        void SaveChanges();
    }
}
