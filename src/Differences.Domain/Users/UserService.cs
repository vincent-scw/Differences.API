using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.B2CGraphClient;
using Differences.Common;
using Differences.Common.Resources;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace Differences.Domain.Users
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly GraphClient _graphClient;

        public UserService(IUserRepository userRepository,
            GraphClient graphClient,
            IStringLocalizer<Errors> localizer)
            : base(localizer)
        {
            _userRepository = userRepository;
            _graphClient = graphClient;
        }

        public User FindOrCreate(Guid globalId, string displayName, string email, string avatarUrl)
        {
            var user = _userRepository.Get(globalId);
            if (user != null)
            {
                return user;
            }

            using (_userRepository.DbContext.Database.BeginTransaction())
            {
                user = new User(globalId, displayName, email, avatarUrl);

                _userRepository.Add(user);
                _userRepository.SaveChanges(); // TODO: should not do this here
            }
            return user;
        }

        public User UpdateUser(Guid globalId, UserModel userModel)
        {
            var user = _userRepository.Get(globalId);
            if (user == null)
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

            if (user.DisplayName != userModel.DisplayName)
            {
                user.Update(userModel.DisplayName);
                _userRepository.SaveChanges();

                var b2cUser = new UserTemplate { DisplayName = userModel.DisplayName };
                var jsonStr = JsonConvert.SerializeObject(b2cUser,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                // Update in Azure AD B2C
                _graphClient.UpdateUser(globalId.ToString(), jsonStr).Wait();
            }

            return user;
        }

        public IReadOnlyList<User> GetTopReputationUsers(int categoryId, int topCount = 20)
        {
            //TODO
            return _userRepository.GetAll().ToList();
        }
    }
}
