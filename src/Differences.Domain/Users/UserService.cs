using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;

namespace Differences.Domain.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserInfo(Guid globalId)
        {
            return _userRepository.Find(x => x.GlobalId == globalId).SingleOrDefault();
        }

        public IEnumerable<User> GetTopReputationUsers(long categoryId, int topCount = 20)
        {
            throw new NotImplementedException();
        }
    }
}
