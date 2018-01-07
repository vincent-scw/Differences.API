using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.Common;
using Differences.Common.Resources;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Differences.OAuth2Provider;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using UserInfo = Differences.OAuth2Provider.UserInfo;

namespace Differences.Domain.Users
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly OAuth2ProviderFactory _providerFactory;

        public UserService(IUserRepository userRepository,
            OAuth2ProviderFactory providerFactory,
            IUserContextService userContextService,
            IStringLocalizer<Errors> localizer)
            : base(userContextService, localizer)
        {
            _userRepository = userRepository;
            _providerFactory = providerFactory;
        }

        public User FindOrCreate()
        {
            var userInfo = UserContextService.GetUserInfo();
            var user = _userRepository.Get(userInfo.Id);
            if (user != null)
            {
                return user;
            }

            _userRepository.UseTransaction(() =>
            {
                user = new User(userInfo.Id, userInfo.DisplayName);

                _userRepository.Add(user);
                _userRepository.SaveChanges(); // TODO: should not do this here
            });
            return user;
        }

        private User FindOrCreateByLinkedInId(UserInfo userInfo)
        {
            var user = _userRepository.GetUserByLinkedInId(userInfo.Id);
            if (user != null)
                return user;

            _userRepository.UseTransaction(() =>
            {
                user = new User(Guid.NewGuid(), userInfo.DisplayName)
                {
                    AvatarUrl = userInfo.AvatarUrl,
                    LinkedInId = userInfo.Id
                };

                _userRepository.Add(user);
                _userRepository.SaveChanges();
            });

            return user;
        }

        private User FindOrCreateByMicrosoftId(UserInfo userInfo)
        {
            var user = _userRepository.GetUserByMicrosoftId(userInfo.Id);
            if (user != null)
                return user;

            _userRepository.UseTransaction(() =>
            {
                user = new User(Guid.NewGuid(), userInfo.DisplayName)
                {
                    AvatarUrl = userInfo.AvatarUrl,
                    MicrosoftId = userInfo.Id
                };

                _userRepository.Add(user);
                _userRepository.SaveChanges();
            });

            return user;
        }

        public User GetUser(string accountType, string code)
        {
            var type = Enum.Parse<AccountType>(accountType, true);
            var provider = _providerFactory.GetProvider(type);

            try
            {
                var response = provider.GetAuthResponse(code);
                switch (type)
                {
                    case AccountType.LinkedIn:
                        return FindOrCreateByLinkedInId(response.UserInfo);
                    case AccountType.Microsoft:
                        return FindOrCreateByMicrosoftId(response.UserInfo);
                }
            }
            catch (InvalidOperationException ioe)
            {
                throw new DefinedException(GetLocalizedResource(ioe.Message));
            }

            return null;
        }

        public User UpdateUser(UserModel userModel)
        {
            var user = _userRepository.Get(UserId);
            if (user == null)
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

            if (user.DisplayName != userModel.DisplayName)
            {
                user.Update(userModel.DisplayName, userModel.Email);
                _userRepository.SaveChanges();
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
