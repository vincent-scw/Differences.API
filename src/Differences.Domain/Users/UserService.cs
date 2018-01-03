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
        private readonly IUserContextService _userContextService;

        public UserService(IUserRepository userRepository,
            IUserContextService userContextService,
            GraphClient graphClient,
            IStringLocalizer<Errors> localizer)
            : base(localizer)
        {
            _userRepository = userRepository;
            _userContextService = userContextService;
            _graphClient = graphClient;
        }

        public User FindOrCreate()
        {
            var userInfo = _userContextService.GetUserInfo();
            var user = _userRepository.Get(userInfo.Id);
            if (user != null)
            {
                return user;
            }

            _userRepository.UseTransaction(() =>
            {
                user = new User(userInfo.Id, userInfo.DisplayName, userInfo.Email, userInfo.AvatarUrl);

                _userRepository.Add(user);
                _userRepository.SaveChanges(); // TODO: should not do this here
            });
            return user;
        }

        public User UpdateUser(UserModel userModel)
        {
            var userInfo = _userContextService.GetUserInfo();
            var user = _userRepository.Get(userInfo.Id);
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
                _graphClient.UpdateUser(userInfo.Id.ToString(), jsonStr).Wait();
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
