using Differences.Interaction.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Differences.Interaction.Repositories
{
    public interface IUserRepository : IRepository
    {
        bool Exists(Guid userId);

        User Get(Guid userId);

        User Add(User user);

        IQueryable<User> GetAll();
    }
}
