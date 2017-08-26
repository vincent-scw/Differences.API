using Differences.IdentityServer.MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.IdentityServer.MongoDb
{
    public interface ILoginService
    {
        bool ValidateCredentials(string username, string password);

        MongoDbUser FindByUsername(string username);
    }

    public class LoginService : ILoginService
    {
        private readonly IRepository _repository;

        public LoginService(IRepository repository)
        {
            _repository = repository;
        }

        public bool ValidateCredentials(string username, string password)
        {
            return _repository.ValidatePassword(username, password);
        }

        public MongoDbUser FindByUsername(string username)
        {
            return _repository.GetUserByUsername(username);
        }
    }
}
