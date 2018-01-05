using System.Threading.Tasks;
using Differences.Domain.Models;
using Differences.OAuth2Provider;

namespace Differences.Domain.Users
{
    public interface IAccountService
    {
        UserWithTokenModel GetAuthResponse(string accountType, string code);
    }
}