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

        public User FindOrCreate(Guid globalId, string displayName, string email, string avatarUrl)
        {
            var user = _userRepository.Get(globalId);
            if (user != null)
                return user;

            user = new User(globalId, displayName, email, avatarUrl);

            _userRepository.Add(user);

            _userRepository.SaveChanges(); // TODO: should not do this here
            return user;
        }

        public IReadOnlyList<User> GetTopReputationUsers(int categoryId, int topCount = 20)
        {
            //TODO
            return _userRepository.GetAll().ToList();
        }
    }
}
