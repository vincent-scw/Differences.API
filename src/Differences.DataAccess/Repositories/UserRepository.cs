using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Differences.DataAccess.Repositories
{
    public class UserRepository : IUserRepository, IRepository
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
            return _dbContext.Users.Include(x => x.UserScores).SingleOrDefault(x => x.Id == userId);
        }

        public User Add(User user)
        {
            user.CreateTime = DateTime.Now;
            user.CreatedBy = user.Id;
            _dbContext.Users.Add(user);
            // Init UserScore when new user added
            _dbContext.UserScores.Add(new UserScore(user.Id));
            return user;
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
