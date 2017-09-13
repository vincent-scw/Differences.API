using AutoMapper;
using Differences.Api.Model;
using Differences.Interaction.Models;

namespace Differences.Api
{
    internal class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<User, UserType>();
            CreateMap<UserType, User>();
        }
    }
}