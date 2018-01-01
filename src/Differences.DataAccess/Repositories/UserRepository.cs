using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using System;
using System.Linq;

namespace Differences.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DifferencesDbContext _dbContext;

        public UserRepository(DifferencesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(Guid userId)
        {
            return _dbContext.Users.Any(x => x.Id == userId);
        }

        public User Get(Guid userId)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Id == userId);
        }

        public User Add(User user)
        {
            user.CreateTime = DateTime.Now;
            user.CreatedBy = user.Id;
            _dbContext.Users.Add(user);
            return user;
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
